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
    /// Represents a single list item.
    /// </summary>
    internal class ListItemBlock
    {
        /// <summary>
        /// Gets or sets the contents of the list item.
        /// </summary>
        public IList<MarkdownBlock> Blocks { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListItemBlock"/> class.
        /// </summary>
        public ListItemBlock()
        {
        }
    }
}
