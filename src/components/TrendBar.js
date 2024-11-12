import React from 'react'
import { IoIosTrendingUp } from "react-icons/io";
import useFetch from '../hooks/useFetch';

const TrendBar = () => {
    const {data: categories, isLoading, error} = useFetch('https://localhost:7040/api/Category')
  return (
    <section className='px-10 py-2 border-b lg:px-20 lg:py-3 border-dark'>
        <div className='flex items-center gap-5'>
            <IoIosTrendingUp className='w-4 h-auto lg:w-6' />
            <div className='flex items-center w-full gap-3 overflow-hidden lg:gap-5'>
                {isLoading && <p>იტვირთება...</p>}
                {error && <p>{error.message}</p>}
                {categories && categories.map((category) => (
                    <div 
                        key={category.id}
                        className='p-2 text-xs rounded-full lg:text-sm bg-dark bg-opacity-10 font-firago whitespace-nowrap'
                    > 
                        {category.name}
                    </div>
                ))}
            </div>
        </div>
    </section>
  )
}

export default TrendBar
