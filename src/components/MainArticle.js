import React from 'react'
import useFetch from '../hooks/useFetch'

const MainArticle = ({id = 'c3e68ea1-f4c7-43e3-a244-fbfdbb715e88'}) => {
    const {data: mainArticle, isLoading, error} = useFetch(`https://localhost:7040/api/Article/${id}`);

  return (
    <section className='w-full lg:h-[500px] h-[250px] bg-dark flex flex-col justify-end relative'>
        {isLoading && <p>იტვირთება...</p>}
        {error && <p>{error.message}</p>}
        {mainArticle &&
            <>
                {mainArticle.photo ? 
                    <img 
                        src={mainArticle.photo} alt={mainArticle.title}
                        className='absolute z-10 object-cover w-full h-full'
                     /> 
                    : 
                    null
                }
                
                <div className='z-20 px-5 py-3 lg:px-10 lg:py-5'>
                    <h1 className='text-xs font-bold text-white lg:text-2xl'> {mainArticle.title} </h1>
                    <div className='flex w-full h-3 mt-2 lg:mt-5 lg:h-8 font-firago case-on'>
                        <div 
                            style={{ width: `${mainArticle.oppCoverage}%` }}
                            className='bg-opp flex justify-center items-center text-white lg:text-base text-[7px] lg:font-semibold'
                        >
                            {mainArticle.oppCoverage < 10 ? '' : mainArticle.oppCoverage < 30 ? mainArticle.oppCoverage + '%' : 'ოპოზიციური ' + mainArticle.oppCoverage + '%' }
                            
                        </div>
                        <div 
                            style={{ width: `${mainArticle.centerCoverage}%` }}
                            className='bg-center flex justify-center items-center text-dark lg:text-base text-[7px] lg:font-semibold'
                        >
                            {mainArticle.centerCoverage < 10 ? '' : mainArticle.centerCoverage < 30 ? mainArticle.centerCoverage + '%' : 'ცენტრისტული ' + mainArticle.centerCoverage + '%' }
                        </div>
                        <div 
                            style={{ width: `${mainArticle.govCoverage}%` }}
                            className='bg-gov flex justify-center items-center text-white lg:text-base text-[7px] lg:font-semibold'
                        >
                            {mainArticle.govCoverage < 10 ? '' : mainArticle.govCoverage < 30 ? mainArticle.govCoverage + '%' : 'სამთავრობო ' + mainArticle.govCoverage + '%' }
                        </div>
                    </div>
                </div>
            </>
        }
    </section>
  )
}

export default MainArticle
