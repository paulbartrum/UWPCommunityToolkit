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

using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.UI.Controls.Markdown.Helpers;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Markdown.Parse.Elements
{
    /// <summary>
    /// Represents a single row in the table.
    /// </summary>
    internal class TableRow
    {
        /// <summary>
        /// Gets or sets a list of table cells.
        /// </summary>
        public IList<TableCell> Cells { get; set; }

        /// <summary>
        /// Parses the contents of the row, ignoring whitespace at the beginning and end of each cell.
        /// </summary>
        /// <param name="markdown"> The markdown text. </param>
        /// <param name="startingPos"> The position of the start of the row. </param>
        /// <param name="maxEndingPos"> The maximum position of the end of the row </param>
        /// <param name="quoteDepth"> The current nesting level for block quoting. </param>
        /// <param name="requireVerticalBar"> Indicates whether the line must contain a vertical bar. </param>
        /// <param name="contentParser"> Called for each cell. </param>
        /// <returns> The position of the start of the next line. </returns>
        internal static int ParseContents(string markdown, int startingPos, int maxEndingPos, int quoteDepth, bool requireVerticalBar, Action<int, int> contentParser)
        {
            // Skip quote characters.
            int pos = Common.SkipQuoteCharacters(markdown, startingPos, maxEndingPos, quoteDepth);

            // If the line starts with a '|' character, skip it.
            bool lineHasVerticalBar = false;
            if (pos < maxEndingPos && markdown[pos] == '|')
            {
                lineHasVerticalBar = true;
                pos++;
            }

            while (pos < maxEndingPos)
            {
                // Ignore any whitespace at the start of the cell (except for a newline character).
                while (pos < maxEndingPos && Common.IsWhiteSpace(markdown[pos]) && markdown[pos] != '\n')
                {
                    pos++;
                }

                int startOfCellContent = pos;

                // Find the end of the cell.
                bool endOfLineFound = true;
                while (pos < maxEndingPos)
                {
                    char c = markdown[pos];
                    if (c == '|')
                    {
                        lineHasVerticalBar = true;
                        endOfLineFound = false;
                        break;
                    }

                    if (c == '\n')
                    {
                        break;
                    }

                    pos++;
                }

                int endOfCell = pos;

                // If a vertical bar is required, and none was found, then exit early.
                if (endOfLineFound && !lineHasVerticalBar && requireVerticalBar)
                {
                    return startingPos;
                }

                // Ignore any whitespace at the end of the cell.
                if (endOfCell > startOfCellContent)
                {
                    while (Common.IsWhiteSpace(markdown[pos - 1]))
                    {
                        pos--;
                    }
                }

                int endOfCellContent = pos;

                if (endOfLineFound == false || endOfCellContent > startOfCellContent)
                {
                    // Parse the contents of the cell.
                    contentParser(startOfCellContent, endOfCellContent);
                }

                // End of input?
                if (pos == maxEndingPos)
                {
                    break;
                }

                // Move to the next cell, or the next line.
                pos = endOfCell + 1;

                // End of the line?
                if (endOfLineFound)
                {
                    break;
                }
            }

            return pos;
        }

        /// <summary>
        /// Called when this block type should parse out the goods. Given the markdown, a starting point, and a max ending point
        /// the block should find the start of the block, find the end and parse out the middle. The end most of the time will not be
        /// the max ending pos, but it sometimes can be. The function will return where it ended parsing the block in the markdown.
        /// </summary>
        /// <param name="markdown"> The markdown text to parse. </param>
        /// <param name="startingPos"> The start of the line. </param>
        /// <param name="maxEndingPos"> The position to stop parsing. </param>
        /// <param name="quoteDepth"> The current nesting level for block quoting. </param>
        /// <returns> The position where parsing stopped. </returns>
        internal int Parse(string markdown, int startingPos, int maxEndingPos, int quoteDepth)
        {
            Cells = new List<TableCell>();
            return ParseContents(
                markdown,
                startingPos,
                maxEndingPos,
                quoteDepth,
                requireVerticalBar: true,
                contentParser: (startingPos2, maxEndingPos2) =>
                {
                    var cell = new TableCell();
                    cell.Inlines = Common.ParseInlineChildren(markdown, startingPos2, maxEndingPos2);
                    Cells.Add(cell);
                });
        }
    }
}
