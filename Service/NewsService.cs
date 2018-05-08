using DataRepository.DataAccess.News;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;

namespace Service
{
    public class NewsService : BaseService
    {
        public static bool ModifiyNews(NewsEntity entity)
        {
            int result = 0;
            if (entity != null)
            {
                NewsRepository mr = new NewsRepository();
                NewsInfo newInfo = TranslateNewsInfo(entity);
                if (entity.ID > 0)
                {
                    result = mr.UpdateNew(newInfo);
                }
                else
                {
                    result = mr.InsertNew(newInfo);
                }

                //List<StoreInfo> miList = mr.InsertNew();//刷新缓存
                //Cache.Add("StoreALL", miList);
            }
            return result > 0;
        }

        public static List<NewsEntity> GetNews(string title,int status)
        {
            List<NewsEntity> lstNews = new List<NewsEntity>();
            NewsRepository mr = new NewsRepository();
            List<NewsInfo> lstInfo = mr.GetNews(title, status);
            if (lstInfo != null && lstInfo.Count > 0)
            {
                foreach (NewsInfo info in lstInfo)
                {
                    NewsEntity entity = new NewsEntity();
                    entity.ID = info.ID;
                    entity.ChannelID = info.ChannelID;
                    entity.Title = info.Title;
                    entity.ImageUrl = info.ImageUrl;
                    entity.zhaiyao = info.zhaiyao;
                    entity.Content = info.Content;
                    entity.Sort = info.Sort;
                    entity.Status = info.Status;
                    entity.Operator = info.Operator;
                    entity.CreateDate = info.CreateDate;
                    entity.ModifyDate = info.ModifyDate;
                    lstNews.Add(entity);
                }
            }
            return lstNews;
        }

        public static List<NewsEntity> GetCountNews(int count)
        {
            List<NewsEntity> lstNews = new List<NewsEntity>();
            NewsRepository mr = new NewsRepository();

            List<NewsInfo> lstInfo = Cache.Get<List<NewsInfo>>("GetCountNews" + count.ToString());
            if (lstInfo.IsEmpty())
            {
                lstInfo = mr.GetCountNews(count);
                Cache.Add("GetCountNews" + count.ToString(), lstInfo);
            }

            if (lstInfo != null && lstInfo.Count > 0)
            {
                foreach (NewsInfo info in lstInfo)
                {
                    NewsEntity entity = new NewsEntity();
                    entity.ID = info.ID;
                    entity.ChannelID = info.ChannelID;
                    entity.Title = info.Title;
                    entity.ImageUrl = info.ImageUrl;
                    entity.zhaiyao = info.zhaiyao;
                    entity.Content = info.Content;
                    entity.Sort = info.Sort;
                    entity.Status = info.Status;
                    entity.Operator = info.Operator;
                    entity.CreateDate = info.CreateDate;
                    entity.ModifyDate = info.ModifyDate;
                    lstNews.Add(entity);
                }
            }
            return lstNews;
        }

        public static NewsEntity GetNewsByID(int ID)
        {
            NewsEntity entity = new NewsEntity();
            NewsRepository mr = new NewsRepository();
            NewsInfo info = mr.GetNewsByID(ID);
            if (info != null)
            {
                entity.ID = info.ID;
                entity.ChannelID = info.ChannelID;
                entity.Title = info.Title;
                entity.ImageUrl = info.ImageUrl;
                entity.zhaiyao = info.zhaiyao;
                entity.Content = info.Content;
                entity.Sort = info.Sort;
                entity.Status = info.Status;
                entity.Operator = info.Operator;
            }
            return entity;
        }

        public static bool RemoveNews(int ID)
        {
            NewsRepository mr = new NewsRepository();
            return mr.DeleteNew(ID) > 1;
        }


        public static NewsInfo TranslateNewsInfo(NewsEntity entity)
        {
            NewsInfo info = new NewsInfo();
            if (entity != null)
            {
                info.ID = entity.ID;
                info.ChannelID = entity.ChannelID;
                info.Title = entity.Title;
                info.zhaiyao = entity.zhaiyao;
                info.Content = entity.Content;
                info.ImageUrl = entity.ImageUrl;
                info.Sort = entity.Sort;
                info.Status = entity.Status;
                info.Operator = entity.Operator;
            }
            return info;
        }
    }
}
