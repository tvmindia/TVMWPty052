using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Services
{
    public class ReviewRepository:IReviewRepository
    {
        Const ConstObj = new Const();

        private IDatabaseFactory _databaseFactory;
        private IAttributesRepository _attributesRepository;

        public ReviewRepository(IDatabaseFactory databaseFactory,IAttributesRepository attributesRepository)
        {
            _databaseFactory = databaseFactory;
            _attributesRepository = attributesRepository;
        }

        public List<ProductReview> GetAllReviews(string Condition,string FromDate,string ToDate)
        {
            List<ProductReview> reviewList = null;
          

            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetAllReviewsandRating]";
                        cmd.Parameters.Add("@Condition", SqlDbType.NVarChar,20).Value = Condition;
                        cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 20).Value = FromDate;
                        cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 20).Value = ToDate;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                reviewList = new List<ProductReview>();
                                while (sdr.Read())
                                {
                                    ProductReview _reviewObj = new ProductReview();
                                    {
                                        
                                        _reviewObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _reviewObj.CustomerID);
                                        _reviewObj.CustomerName = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _reviewObj.CustomerName);
                                        _reviewObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString() ): _reviewObj.ProductID);
                                        _reviewObj.ProductName = (sdr["ProductName"].ToString() != "" ? sdr["ProductName"].ToString() : _reviewObj.ProductName);
                                        _reviewObj.Review = (sdr["Review"].ToString() != "" ? sdr["Review"].ToString() : _reviewObj.Review);
                                        _reviewObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : _reviewObj.AttributeSetID);
                                        _reviewObj.ReviewCreatedDate = (sdr["ReviewDate"].ToString() != "" ? DateTime.Parse(sdr["ReviewDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _reviewObj.ReviewCreatedDate);
                                        _reviewObj.RatingDate = (sdr["RatingDate"].ToString() != "" ? DateTime.Parse(sdr["RatingDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _reviewObj.RatingDate);
                                        _reviewObj.IsApproved = (sdr["ApprovedYN"].ToString() != "" ? sdr["ApprovedYN"].ToString() : _reviewObj.IsApproved);
                                        


                                    }
                                    reviewList.Add(_reviewObj);
                                }
                            }//if
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reviewList;


        }

        public List<ProductReview> GetProductRatingByCustomer(int ProductID, int CustomerID, int AttributesetId)
        {
            List<ProductReview> RatingSummary = null;
            List<AttributeValues> myAttributeStructure = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[GetProductRatingByCustomer]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;


                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                RatingSummary = new List<ProductReview>();
                                while (sdr.Read())
                                {
                                    ProductReview _pReviewObj = new ProductReview();
                                    {
                                        _pReviewObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _pReviewObj.ProductID);
                                        //_pReviewObj.RatingCount = (sdr["RatingCount"].ToString() != "" ? sdr["RatingCount"].ToString() : _pReviewObj.RatingCount);

                                        if (myAttributeStructure == null)
                                        {
                                            myAttributeStructure = _attributesRepository.GetAttributeContainer(AttributesetId, "Rating");
                                        }

                                        _pReviewObj.ProductRatingAttributes = new List<AttributeValues>();
                                        foreach (AttributeValues att in myAttributeStructure)
                                        {
                                            att.Value = sdr[att.Caption].ToString();
                                            _pReviewObj.ProductRatingAttributes.Add(att);

                                        }
                                    }
                                    RatingSummary.Add(_pReviewObj);
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return RatingSummary;
        }
    }
}