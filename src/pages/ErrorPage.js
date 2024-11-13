import React from 'react'
import MainLayout from '../components/main layout/MainLayout'

const ErrorPage = () => {
  return (
    <MainLayout>
        <section className='mx-auto py-36 w-fit'>
            <div className='flex gap-5 text-xl font-firago'>
              <p className='text-dark'>404</p>
              <div className='border-r border-dark' />
              <p className='text-dark'>გვერდი ვერ მოიძებნა</p>
            </div>
        </section>
    </MainLayout>
  )
}

export default ErrorPage
