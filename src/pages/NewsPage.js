import React from 'react'
import MainLayout from '../components/main layout/MainLayout'
import PublisherBar from '../components/PublisherBar'
import BlindspotBar from '../components/BlindspotBar'
import Articles from '../components/Articles'
import TopNewsBar from '../components/TopNewsBar'

const NewsPage = () => {
  return (
    <MainLayout>
        <div className='px-8 py-3 lg:px-20 lg:py-10'>
            <div className='flex flex-col justify-between gap-5 lg:flex-row'>
                <div className='lg:w-[80%] w-full'>
                    <Articles />
                </div>

                <div className='border-b lg:border-r border-dark' />

                <div className='lg:w-[20%] w-full flex flex-col gap-5'>
                    <TopNewsBar />
                    <div className='border-b border-dark' />
                    <PublisherBar />
                    <div className='border-b border-dark' />
                    <BlindspotBar />
                </div>
            </div>
        </div>
    </MainLayout>
  )
}

export default NewsPage
