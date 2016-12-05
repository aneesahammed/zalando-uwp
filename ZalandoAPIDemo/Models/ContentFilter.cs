using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoAPIDemo.Enums;

namespace ZalandoAPIDemo.Models
{
    public class ContentFilter
    {
        public AgeGroup? AgeGroup;
        public Color? Color;
        public Gender? Gender;
        public string FullText;

        public string GetFilter()
        {

            try
            {
                var sb = new StringBuilder();

                if (this.AgeGroup != null)
                {
                    var ageGroups = this.AgeGroup.ToString().Split(',');
                    foreach (var ageGroup in ageGroups)
                    {
                        sb.Append($"&ageGroup={ageGroup.Trim()}");
                    }
                }

                if (this.Color != null)
                {
                    var colors = this.Color.ToString().Split(',');
                    foreach (var color in colors)
                    {
                        sb.Append($"&color={color.Trim()}");
                    }
                }

                if (this.Gender != null)
                {
                    var genders = this.Gender.ToString().Split(',');
                    foreach (var gender in genders)
                    {
                        sb.Append($"&gender={gender.Trim()}");
                    }
                }

                if (!string.IsNullOrEmpty(this.FullText))
                {
                    sb.Append($"&fulltext={this.FullText}");
                }

                if(sb.Length>0)
                {
                    sb.Remove(0, 1);
                    sb.Insert(0, "?");
                }               
                

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public string GetFilter(CustomFilter option)
        {
            try
            {
                var sb = new StringBuilder();

                switch (option)
                {
                    case CustomFilter.All:                        
                        break;
                    case CustomFilter.Men:
                        sb.Append($"&gender={Enums.Gender.Male}");
                        break;
                    case CustomFilter.Women:
                        sb.Append($"&gender={Enums.Gender.Female}");
                        break;
                    case CustomFilter.Kids:
                        sb.Append($"&ageGroup={option}");
                        break;

                    default:
                        break;
                }

                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    sb.Insert(0, "?");
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
