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

namespace Microsoft.Toolkit.Uwp.UI.Controls.Markdown.Parse.Elements
{
    /// <summary>
    /// The alignment of content in a table column.
    /// </summary>
    internal enum ColumnAlignment
    {
        /// <summary>
        /// The alignment was not specified.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Content should be left aligned.
        /// </summary>
        Left,

        /// <summary>
        /// Content should be right aligned.
        /// </summary>
        Right,

        /// <summary>
        /// Content should be centered.
        /// </summary>
        Center,
    }

    /// <summary>
    /// Describes a column in the markdown table.
    /// </summary>
    internal class TableColumnDefinition
    {
        public ColumnAlignment Alignment { get; set; }
    }
}
