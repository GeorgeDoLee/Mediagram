import React from 'react'
import useFetch from '../hooks/useFetch'


const TopNewsBar = () => {
    const {data: articles, isLoading, error} = useFetch('https://localhost:7040/api/Article')

  return (
    <section className='w-full'>
        <h1 className='mb-3 text-xl font-semibold font-firago case-on'>ტრენდული ამბები</h1>

        {error && <p>{error}</p>}
        {isLoading && <p>Loading...</p>}
        <div className='flex flex-col gap-5 text-dark font-firago'>
            {articles && articles.slice(0, 5).map((article, index) => {
                const { govCoverage, centerCoverage, oppCoverage } = article;
                let mostCoverage = { percentage: 0, position: ''};

                if (govCoverage >= centerCoverage && govCoverage >= oppCoverage) {
                    mostCoverage = { percentage: govCoverage, position: 'სამთავრობო' };
                } else if (oppCoverage >= centerCoverage && oppCoverage >= govCoverage) {
                    mostCoverage = { percentage: oppCoverage, position: 'ოპოზიციური' };
                } else {
                    mostCoverage = { percentage: centerCoverage, position: 'ცენტრისტული' };
                }
                return (
                    <div key={index} >
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
            })}

        </div>
    </section>
  )
}

export default TopNewsBar
