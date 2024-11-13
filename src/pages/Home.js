import React from 'react'
import MainLayout from "../components/main layout/MainLayout"
import BlindspotBar from '../components/BlindspotBar'
import MainArticle from '../components/MainArticle'
import TopNewsBar from '../components/TopNewsBar'
import Articles from '../components/Articles'
import PublisherBar from '../components/PublisherBar'

const Home = () => {  
  return (
    <MainLayout>
      <div className='flex flex-col gap-5 px-8 py-3 lg:px-20 lg:py-10'>
        <div className='flex flex-col justify-between gap-5 lg:flex-row'>
          <div className='lg:w-[75%] w-full flex items-center justify-center'>
            <MainArticle />
          </div>

          <div className='border-b lg:border-r border-dark' />

          <div className='lg:w-[25%] w-full'>
            <TopNewsBar />
          </div>
        </div>

        <div className='border-b border-dark' />

        <div className='flex flex-col justify-between gap-5 lg:flex-row'>
          <div className='lg:w-[20%] w-full flex flex-col gap-5'>
            <PublisherBar />
            <div className='border-b border-dark' />
            <BlindspotBar />
          </div>
          
          <div className='border-b lg:border-r border-dark' />

          <div className='lg:w-[80%] w-full'>
            <Articles />
          </div>
        </div>
      </div>
    </MainLayout>
  )
}

export default Home
