using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace EnterTheMatrix
{
    public partial class _default : System.Web.UI.Page
    {
        Color BookmarkColor = Color.Gold;
        protected void Page_Load(object sender, EventArgs e)
        {
                        
            if (IsPostBack && Session["queryResult"] != null)
            {
                RenderRepositories();
            }

        }


        /// <summary>
        /// renders results from api.github.com to screen via term
        /// </summary>
        /// <param name="term">query search term</param>
        protected void RenderRepositories(string term)
        {
            div_results.Controls.Clear();
            List<GithubRepo> repositories = new GithubServices().GetRepositories(term);
            Session["queryResult"] = repositories;
            List<HtmlGenericControl> results = new List<HtmlGenericControl>();
            foreach (GithubRepo repo in repositories)
            {
                HtmlGenericControl div = CreateRepositoryDiv(repo);
                results.Add(div);
                div_results.Controls.Add(div);
            }
            if (results.Count < 1)
            {
                NoResultsError();
            }

        }

        public void NoResultsError()
        {
            HtmlGenericControl h2 = new HtmlGenericControl("h2");
            h2.InnerText = "No results for the requested term, please try again";
            div_results.Controls.Add(h2);
        }

        /// <summary>
        /// renders the results stored in session.
        /// used to re render after callback
        /// </summary>
        protected void RenderRepositories()
        {
            div_results.Controls.Clear();
            List<GithubRepo> results = (List<GithubRepo>)Session["queryResult"];
            foreach (GithubRepo repo in results)
            {
                HtmlGenericControl div = CreateRepositoryDiv(repo);
                div_results.Controls.Add(div);
            }
            if (results.Count < 1)
            {
                NoResultsError();
            }

        }

        /// <summary>
        /// Create the gallery item  with the name avatar and bookmark button
        /// </summary>
        /// <param name="repo">GithubRepo object with information for the gallery item</param>
        /// <returns>div with all the gallery item controls</returns>
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

            //button
            Button btn_bookmark = new Button();
            btn_bookmark.ID = repo.id.ToString();
            btn_bookmark.Text = "Bookmark";
            btn_bookmark.Click += Btn_bookmark_Click;
            if (CheckIfBookmarked(repo.id))
            {
                btn_bookmark.BackColor = BookmarkColor;
            }
            //btn_bookmark.Attributes.Add("onclick", "btn_search_click");            





            HtmlGenericControl innerDiv = new HtmlGenericControl("div");
            innerDiv.Controls.Add(textbox);
            div.Controls.Add(innerDiv);
            div.Controls.Add(ownerImg);
            div.Controls.Add(btn_bookmark);
            //div_results.Controls.Add(div);
            return div;
        }

        private void Btn_bookmark_Click(object sender, EventArgs e)
        {
            if (sender is Button _sender)
            {
                int _id = int.Parse(_sender.ClientID);
                bool isBookmarked = CheckIfBookmarked(_id);
                if (isBookmarked)
                {
                    RemoveFromBookmarks(_id);
                    _sender.BackColor = default(Color);
                }
                else
                {
                    AddToBookmarks(_id);                 
                    _sender.BackColor = BookmarkColor;
                }

              
            }

        }

        /// <summary>
        /// removed a repository from session bookmarks
        /// </summary>
        /// <param name="id">id of the removed bookmark repo</param>
        protected void RemoveFromBookmarks(int id)
        {
            if (Session["bookmarks"] == null)
                return;

            List<GithubRepo> bookmarks = (List<GithubRepo>)Session["bookmarks"];
            foreach (GithubRepo repo in bookmarks)
            {
                if (repo.id==id)
                {
                    bookmarks.Remove(repo);
                    break;
                }

            }
            Session["bookmarks"] = bookmarks;

        }

        /// <summary>
        /// adds a repository to session bookmarks
        /// </summary>
        /// <param name="id">repository id to be added</param>
        protected void AddToBookmarks(int id)
        {
            List<GithubRepo> bookmarks;
            if (Session["bookmarks"] == null)
                bookmarks = new List<GithubRepo>();
            else
                bookmarks = (List<GithubRepo>)Session["bookmarks"];

            
                foreach (GithubRepo repo in (List<GithubRepo>)Session["queryResult"])
                {
                    if (repo.id == id)
                    {
                        bookmarks.Add(repo);
                        break;
                    }
                }

                Session["bookmarks"] = bookmarks;
           

        }

        protected bool CheckIfBookmarked(int id)
        {

            if (Session["bookmarks"] != null)
            {
                List<GithubRepo> bookmarks = (List<GithubRepo>)Session["bookmarks"];
                foreach (GithubRepo repo in bookmarks)
                {
                    if (repo.id == id)
                        return true;
                }
            }

            return false;
        }


        protected void btn_search_click(object sender, EventArgs e)
        {
            RenderRepositories(tbSearch.Text);
        }


    }
}