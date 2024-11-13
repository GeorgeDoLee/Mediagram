import React from 'react'
import useFetch from '../hooks/useFetch'
import useCoverageCalculator from '../hooks/useCoverageCalculator';
import { Link } from 'react-router-dom';

const BlindspotArticle = ({article}) => {
    const mostCoverage = useCoverageCalculator(article)

    return (
        <div 
            key={article.id} 
            className={`p-2 rounded-lg ${mostCoverage.position === 'opp' ? 'bg-opp' : 'bg-gov'}`}
        >
            {article.photo ? 
                <img src={article.photo} alt={article.title}
                    className='w-full h-[150px] object-cover'
                />
                :
                <div className='w-full h-[150px] bg-dark rounded-t-md bg-opacity-80' />
            }
            <div className='p-2 rounded-b-md bg-newspaper'>
                <h1 className='text-sm font-semibold line-clamp-3 font-firago case-on'>{article.title}</h1>
                <div className='flex items-center gap-2 mt-1'>
                    <div className='flex w-1/4 h-2'>
                        <div style={{ width: `${article.oppCoverage}%` }} className="h-full bg-opp" />
                        <div style={{ width: `${article.centerCoverage}%` }} className="h-full bg-center" />
                        <div style={{ width: `${article.govCoverage}%` }} className="h-full bg-gov" />
                    </div>
                    <p className='text-xs font-firago'>
                        {mostCoverage.percentage}% {mostCoverage.position} წყარო: {article.subArticleCount} სტატია
                    </p>
                </div>
            </div>
        </div>
    )
}

const BlindspotBar = () => {
    const { data: articles, isLoading, error } = useFetch('https://localhost:7040/api/Article?isBlindSpot=true&pageNumber=1&pageSize=5&sortByTrending=true');

  return (
    <section className='w-full'>
        <h1 className='mb-3 text-xl font-semibold text-dark font-firago case-on'>ბრმა წერტილები</h1>
        
        {error && <p>{error}</p>}
        {isLoading && <p>Loading...</p>}
        <div className='flex flex-col gap-5 text-dark font-firago'>
            {articles?.articles && articles.articles.slice(0, 4).map((article) => 
                <BlindspotArticle article={article} />
            )}
            <Link 
                to='/blindspots'
                className='self-center px-4 py-2 text-base font-semibold border w-fit text-dark border-dark font-firago case-on'
            >
                მეტის ნახვა
            </Link>
        </div>
    </section>
  )
}

export default BlindspotBar
