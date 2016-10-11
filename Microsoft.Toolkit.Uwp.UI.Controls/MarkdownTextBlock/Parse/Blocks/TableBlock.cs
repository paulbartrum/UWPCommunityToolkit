// Copyright (c) 2016 Quinn Damerell
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Markdown.Parse.Elements
{
    /// <summary>
    /// Represents a block which contains tabular data.
    /// </summary>
    internal class TableBlock : MarkdownBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableBlock"/> class.
        /// </summary>
        public TableBlock()
            : base(MarkdownBlockType.Table)
        {
        }

        /// <summary>
        /// Gets or sets the table rows.
        /// </summary>
        public IList<TableRow> Rows { get; set; }

        /// <summary>
        /// Gets or sets a list of columns in the table.  Rows can have more or less cells than the
        /// number of columns.  Rows with fewer cells should be padded with empty cells.  For rows
        /// with more cells, the extra cells should be hidden.
        /// </summary>
        public IList<TableColumnDefinition> ColumnDefinitions { get; set; }

        /// <summary>
        /// Parses a table block.
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <param name="start"> The location of the first character in the block. </param>
        /// <param name="endOfFirstLine"> The location of the end of the first line. </param>
        /// <param name="maxEnd"> The location to stop parsing. </param>
        /// <param name="quoteDepth"> The current nesting level for block quoting. </param>
        /// <param name="actualEnd"> Set to the end of the block when the return value is non-null. </param>
        /// <returns> A parsed table block, or <c>null</c> if this is not a table block. </returns>
        internal static TableBlock Parse(string markdown, int start, int endOfFirstLine, int maxEnd, int quoteDepth, out int actualEnd)
        {
            // A table is a line of text, with at least one vertical bar (|), followed by a line of
            // of text that consists of alternating dashes (-) and vertical bars (|) and optionally
            // vertical bars at the start and end.  The second line must have at least as many
            // interior vertical bars as there are interior vertical bars on the first line.
            actualEnd = start;

            // First thing to do is to check if there is a vertical bar on the line.
            int barOrNewLineIndex = markdown.IndexOf('|', start, endOfFirstLine - start);
            if (barOrNewLineIndex < 0)
            {
                return null;
            }

            var rows = new List<TableRow>();

            // Parse the first row.
            var firstRow = new TableRow();
            start = firstRow.Parse(markdown, start, maxEnd, quoteDepth);
            rows.Add(firstRow);

            // Parse the contents of the second row.
            var secondRowContents = new List<string>();
            start = TableRow.ParseContents(
                markdown,
                start,
                maxEnd,
                quoteDepth,
                requireVerticalBar: false,
                contentParser: (start2, end2) => secondRowContents.Add(markdown.Substring(start2, end2 - start2)));

            // There must be at least as many columns in the second row as in the first row.
            if (secondRowContents.Count < firstRow.Cells.Count)
            {
                return null;
            }

            // Check each column definition.
            // Note: excess columns past firstRowColumnCount are ignored and can contain anything.
            var columnDefinitions = new List<TableColumnDefinition>(firstRow.Cells.Count);
            for (int i = 0; i < firstRow.Cells.Count; i++)
            {
                var cellContent = secondRowContents[i];
                if (cellContent.Length == 0)
                {
                    return null;
                }

                // The first and last characters can be '-' or ':'.
                if (cellContent[0] != ':' && cellContent[0] != '-')
                {
                    return null;
                }

                if (cellContent[cellContent.Length - 1] != ':' && cellContent[cellContent.Length - 1] != '-')
                {
                    return null;
                }

                // Every other character must be '-'.
                for (int j = 1; j < cellContent.Length - 1; j++)
                {
                    if (cellContent[j] != '-')
                    {
                        return null;
                    }
                }

                // Record the alignment.
                var columnDefinition = new TableColumnDefinition();
                if (cellContent.Length > 1 && cellContent[0] == ':' && cellContent[cellContent.Length - 1] == ':')
                {
                    columnDefinition.Alignment = ColumnAlignment.Center;
                }
                else if (cellContent[0] == ':')
                {
                    columnDefinition.Alignment = ColumnAlignment.Left;
                }
                else if (cellContent[cellContent.Length - 1] == ':')
                {
                    columnDefinition.Alignment = ColumnAlignment.Right;
                }

                columnDefinitions.Add(columnDefinition);
            }

            // Parse additional rows.
            while (start < maxEnd)
            {
                var row = new TableRow();
                start = row.Parse(markdown, start, maxEnd, quoteDepth);
                if (row.Cells.Count == 0)
                {
                    break;
                }

                rows.Add(row);
            }

            actualEnd = start;
            return new TableBlock { ColumnDefinitions = columnDefinitions, Rows = rows };
        }
    }
}
