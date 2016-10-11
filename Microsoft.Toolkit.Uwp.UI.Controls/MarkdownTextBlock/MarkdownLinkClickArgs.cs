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

namespace Microsoft.Toolkit.Uwp.UI.Controls
{
    /// <summary>
    /// Provides event data for the LinkClick event.
    /// </summary>
    public class MarkdownLinkClickArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the target of the link that was clicked.  This is generally a URL but
        /// there are a few cases where it isn't.
        /// </summary>
        public string LinkTarget { get; set; }
    }
}
