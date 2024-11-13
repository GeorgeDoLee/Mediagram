import React, { useEffect } from 'react'
import Header from './Header'
import Footer from './Footer'
import TrendBar from './TrendBar'
import { useLocation } from 'react-router-dom'
import ToastManager from './Toast'

const MainLayout = ({children}) => {
  const { pathname } = useLocation();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [pathname]);
  
  return (
    <div className='flex flex-col min-h-screen bg-newspaper'>
      <Header />
      <TrendBar />
      <div className='flex-grow '>
        {children}
      </div>
      <Footer />
      <ToastManager />
    </div>
  )
}

export default MainLayout