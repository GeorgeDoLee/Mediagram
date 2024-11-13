import React from 'react'
import MainLayout from '../components/main layout/MainLayout'
import Articles from '../components/Articles'
import TopNewsBar from '../components/TopNewsBar'
import PublisherBar from '../components/PublisherBar'

const BlindspotsPage = () => {
  return (
    <MainLayout>
        <div className='px-8 py-3 lg:px-20 lg:py-10'>
            <div className='flex flex-col justify-between gap-5 lg:flex-row'>
                <div className='lg:w-[80%] w-full'>
                    <Articles isBlindspot={true} />
                </div>

                <div className='border-b lg:border-r border-dark' />

                <div className='lg:w-[20%] w-full flex flex-col gap-5'>
                    <TopNewsBar />
                    <div className='border-b border-dark' />
                    <PublisherBar />
                </div>
            </div>
        </div>
    </MainLayout>
  )
}

export default BlindspotsPage
