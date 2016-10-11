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
    /// The type of inline element.
    /// </summary>
    internal enum MarkdownInlineType
    {
        /// <summary>
        /// A basic text run.
        /// </summary>
        TextRun,

        /// <summary>
        /// Bold text.
        /// </summary>
        Bold,

        /// <summary>
        /// Italic text.
        /// </summary>
        Italic,

        /// <summary>
        /// A markdown link, with text and a URL.
        /// </summary>
        MarkdownLink,

        /// <summary>
        /// A straight URL.
        /// </summary>
        RawHyperlink,

        /// <summary>
        /// A subreddit link.
        /// </summary>
        RawSubreddit,

        /// <summary>
        /// Strikethrough text.
        /// </summary>
        Strikethrough,

        /// <summary>
        /// Superscript text.
        /// </summary>
        Superscript,

        /// <summary>
        /// Code.
        /// </summary>
        Code,
    }

    /// <summary>
    /// The base class of all inline elements.
    /// </summary>
    internal abstract class MarkdownInline : MarkdownElement
    {
        /// <summary>
        /// Gets or sets the type of inline element.
        /// </summary>
        internal MarkdownInlineType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownInline"/> class.
        /// </summary>
        internal MarkdownInline(MarkdownInlineType type)
        {
            Type = type;
        }
    }
}
