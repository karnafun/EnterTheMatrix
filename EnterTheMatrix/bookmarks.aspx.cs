using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EnterTheMatrix
{
    public partial class bookmarks : System.Web.UI.Page
    {
        Color BookmarkColor = Color.Gold;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["bookmarks"] != null && ((List<GithubRepo>)Session["bookmarks"]).Count>0)            
                RenderBookmarks();            
            else            
                NoBookmarksError();           
        }

        private void NoBookmarksError()
        {
            HtmlGenericControl h2 = new HtmlGenericControl("h2");
            h2.InnerText = "No bookmarks selected, go back to the search page in order to add them";
            div_results.Controls.Add(h2);
        }

        protected void RenderBookmarks()
        {
            List<GithubRepo> bookmarks = (List<GithubRepo>)Session["bookmarks"];
            div_results.Controls.Clear();
            foreach (GithubRepo repo in bookmarks)
            {
                HtmlGenericControl div = CreateRepositoryDiv(repo);
                div_results.Controls.Add(div);
            }

        }
        protected HtmlGenericControl CreateRepositoryDiv(GithubRepo repo)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes["class"] = "resultDiv";
            div.Attributes["class"] = "col-sm-4";
            HtmlGenericControl textbox = new HtmlGenericControl("tb");
            textbox.InnerText = repo.name;
            textbox.Attributes["class"] = "resultName";
            System.Web.UI.HtmlControls.HtmlImage ownerImg = new HtmlImage();
            ownerImg.Src = repo.owner.avatar_url;
            ownerImg.Attributes["class"] = "ownerImg";          
            Button btn_bookmark = new Button();
            btn_bookmark.ID = repo.id.ToString();
            btn_bookmark.Text = "Bookmark";
            btn_bookmark.Click += RemoveFromBookmarks;
            btn_bookmark.BackColor = BookmarkColor;
            HtmlGenericControl innerDiv = new HtmlGenericControl("div");
            innerDiv.Controls.Add(textbox);
            div.Controls.Add(innerDiv);
            div.Controls.Add(ownerImg);
            div.Controls.Add(btn_bookmark);          
            return div;
        }

        protected void RemoveFromBookmarks(object sender, EventArgs e)
        {
            if (Session["bookmarks"] == null)
                return;

            if (sender is Button _sender)
            {
                int _id = int.Parse(_sender.ClientID);
                List<GithubRepo> bookmarks = (List<GithubRepo>)Session["bookmarks"];
                foreach (GithubRepo repo in bookmarks)
                {
                    if (repo.id == _id)
                    {
                        bookmarks.Remove(repo);
                        break;
                    }
                }
                Session["bookmarks"] = bookmarks;
                    RenderBookmarks();

                if (bookmarks.Count < 1)
                    NoBookmarksError();                
            }
        }
    }
}