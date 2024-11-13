import React from 'react'
import useFetch from '../hooks/useFetch'
import useCoverageCalculator from '../hooks/useCoverageCalculator'

const TopArticle = ({article}) => {
    const mostCoverage = useCoverageCalculator(article);

    return (
        <div key={article.id} >
            <h1 className='text-sm line-clamp-3 case-on'>{article.title}</h1>
            <div className='flex items-center gap-2 mt-1'>
                <div className='flex w-1/4 h-2'>
                    <div style={{ width: `${article.oppCoverage}%` }} className="h-full bg-opp" />
                    <div style={{ width: `${article.centerCoverage}%` }} className="h-full bg-center" />
                    <div style={{ width: `${article.govCoverage}%` }} className="h-full bg-gov" />
                </div>
                <p className='text-xs'>{mostCoverage.percentage}% {mostCoverage.position} წყარო</p>
            </div>
        </div>
    )
}

const TopNewsBar = () => {
    const {data: articles, isLoading, error} = useFetch('https://localhost:7040/api/Article?pageNumber=1&pageSize=8&sortByTrending=true')

  return (
    <section className='w-full'>
        <h1 className='mb-3 text-xl font-semibold font-firago case-on'>ტრენდული ამბები</h1>

        <div className='flex flex-col gap-5 text-dark font-firago'>
            {error && <p>{error}</p>}
            {isLoading && <p>Loading...</p>}
            {articles?.articles && articles.articles.slice(0, 5).map((article) => 
                <TopArticle article={article} />
            )}
        </div>
    </section>
  )
}

export default TopNewsBar
