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

namespace Microsoft.Toolkit.Uwp.UI.Controls.Markdown.Parse
{
    /// <summary>
    /// The type of block.
    /// </summary>
    internal enum MarkdownBlockType
    {
        /// <summary>
        /// The root of the parsed markdown document.
        /// </summary>
        Root,

        /// <summary>
        /// A paragraph block.
        /// </summary>
        Paragraph,

        /// <summary>
        /// A quote block.
        /// </summary>
        Quote,

        /// <summary>
        /// A code block.
        /// </summary>
        Code,

        /// <summary>
        /// A heading block.
        /// </summary>
        Header,

        /// <summary>
        /// A list block.
        /// </summary>
        List,

        /// <summary>
        /// A list item builder (used internally during parsing).
        /// </summary>
        ListItemBuilder,

        /// <summary>
        /// A horizontal rule.
        /// </summary>
        HorizontalRule,

        /// <summary>
        /// A table block.
        /// </summary>
        Table,

        /// <summary>
        /// The target of a reference.
        /// </summary>
        LinkReference,
    }

    /// <summary>
    /// The base class for all block elements.
    /// </summary>
    internal abstract class MarkdownBlock : MarkdownElement
    {
        /// <summary>
        /// Gets or sets the type of element.
        /// </summary>
        internal MarkdownBlockType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownBlock"/> class.
        /// </summary>
        internal MarkdownBlock(MarkdownBlockType type)
        {
            Type = type;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns> <c>true</c> if the specified object is equal to the current object; otherwise, <c>false.</c> </returns>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj) || !(obj is MarkdownBlock))
            {
                return false;
            }

            return Type == ((MarkdownBlock)obj).Type;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns> A hash code for the current object. </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Type.GetHashCode();
        }
    }
}
