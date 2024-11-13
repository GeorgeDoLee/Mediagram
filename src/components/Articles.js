import React, { useEffect, useState } from 'react'
import useFetch from '../hooks/useFetch'
import { useNavigate } from 'react-router-dom';
import { toast } from './main layout/Toast';
import { deleteArticle } from '../services/apiServices';
import useCoverageCalculator from '../hooks/useCoverageCalculator';
import { useSelector } from 'react-redux';

const Article = ({ article, isBlindspot, admin, refetch }) => {
    const navigate = useNavigate();
    const mostCoverage = useCoverageCalculator(article)
    const userInfo = useSelector(state => state.user.userInfo);

    const handleDelete = async (id, title) => {
        if(admin && window.confirm(`ნამდვილად გსურთ წაშალოთ სტატია "${title}"?`)){
            try {
                await deleteArticle(id, userInfo.token);
                toast.success('სტატია წაიშალა წარმატებით')
                setTimeout(() => {
                    refetch();
                }, 1000);
            } catch (error) {
                toast.error('მოხდა შეცდომა')
            }
        }
    }

    return (
        <div 
            key={article.id} 
            className={`${isBlindspot ? (mostCoverage.position === 'სამთავრობო' ? 'bg-gov' : 'bg-opp') : 'bg-transparent'} 
            ${isBlindspot ? 'p-2' : ''} 
            w-[full] flex justify-between flex-col-reverse items-center gap-2 rounded-md lg:gap-3 lg:flex-row text-dark`}
        >
            <div className={`${isBlindspot ? 'bg-newspaper rounded-md p-2' : 'bg-transparent'} w-full`}>
                <p className='mb-1 text-xs lg:mb-2 lg:text-sm case-on'>{article.categoryName}</p>
                <h1 
                    onClick={() => navigate(`/article/${article.id}`)}
                    className='text-base lg:text-lg text-dark line-clamp-4'
                >
                    {article.title}
                </h1>
                
                <div className='flex flex-col items-start gap-1 mt-2 lg:items-center lg:gap-2 lg:flex-row'>
                    <div className='flex w-1/5 h-2'>
                        <div style={{ width: `${article.oppCoverage}%` }} className="h-full bg-opp" />
                        <div style={{ width: `${article.centerCoverage}%` }} className="h-full bg-center" />
                        <div style={{ width: `${article.govCoverage}%` }} className="h-full bg-gov" />
                    </div>
                    <p className='text-xs lg:text-sm'>
                        {mostCoverage.percentage}% {mostCoverage.position} წყარო: {article.subArticleCount} სტატია
                    </p>
                </div>
                {admin && 
                    <div className='flex items-center gap-10 mt-3'>
                        <button className='text-base text-dark'>რედაქტირება</button>
                        <button onClick={() => handleDelete(article.id, article.title)} className='text-base text-red-500'>წაშლა</button>
                    </div>
                }
            </div>
            {article.photo ? 
                <img 
                    onClick={() => navigate(`/article/${article.id}`)}
                    src={article.photo} 
                    alt={article.title} 
                    className='w-[250px] h-auto'
                />
                :
                <div className='lg:w-[250px] w-full bg-dark h-[200px] lg:h-auto' /> 
            }
        </div>
    );
}

const Articles = ({ isBlindspot = false, admin }) => {
    const [page, setPage] = useState(1);
    const [allArticles, setAllArticles] = useState([]);
    const { data: articles, isLoading, error, refetch } = useFetch(
        isBlindspot
            ? `https://localhost:7040/api/Article?isBlindSpot=true&pageNumber=${page}&pageSize=10`
            : `https://localhost:7040/api/Article?pageNumber=${page}&pageSize=10`
    );

    useEffect(() => {
        refetch();
    }, [page]);

    useEffect(() => {
        if (articles?.articles) {
            setAllArticles((prevArticles) => [
                ...prevArticles,
                ...articles.articles.filter((newArticle) =>
                    !prevArticles.some((article) => article.id === newArticle.id)
                ),
            ]);
        }
    }, [articles]);

    const loadMoreArticles = () => {
        setPage((prevPage) => prevPage + 1);
    };

    return (
        <section>
            <div className='flex flex-col gap-5 mt-2 lg:gap-10 text-dark font-firago'>
                {isLoading && <p>loading...</p>}
                {error && <p>{error}</p>}
                {allArticles.map((article) => (
                    <Article key={article.id} article={article} isBlindspot={isBlindspot} admin={admin} refetch={refetch} />
                ))}
                {articles?.hasNextPage && 
                    <button
                        onClick={loadMoreArticles}
                        className='self-center px-5 py-2 text-base font-semibold border w-fit text-dark border-dark font-firago case-on'
                    >
                        მეტის ნახვა
                    </button>
                }
            </div>
        </section>
    );
};

export default Articles
