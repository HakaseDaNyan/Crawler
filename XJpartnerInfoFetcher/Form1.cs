using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp;
using AngleSharp.Html.Dom;

namespace XJpartnerInfoFetcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var url = txtUrl.Text;
            var selector = txtSelector.Text;

            var titles = await getTitles(url, selector);
            foreach (var title in titles)
            {
                textBox1.AppendText(title + Environment.NewLine);
            }

        }

        /**
         * 指定されたURLからスクレイピングを行う
         */
        private async Task<System.Collections.Generic.IEnumerable<string>> getTitles(string url, string selector)
        {
            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();

            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(url);

            // Perform the query to get all cells with the content
            var cells = document.QuerySelectorAll(selector);

            // We are only interested in the text - select it with LINQ m.TextContent
            var titles = cells.Select(m => m.GetAttribute("src"));

            return titles;
        }
    }
}
