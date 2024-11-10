import React from 'react'
import MainLayout from '../components/MainLayout'
import PublisherManager from '../components/PublisherManager'
import Articles from '../components/Articles'
import { Link } from 'react-router-dom';
import CategoriesManager from '../components/CategoriesManager';

const AdminPage = () => {

  return (
    <MainLayout>
        <section className='px-20 py-5'>
          <div className='flex flex-col gap-5'>
            <div className='flex gap-2'>
              <Link to="/admin/publisher" className='self-start px-4 py-2 text-base border border-dark text-dark'>მედიის დამატება</Link>
              <Link to="/admin/article" className='self-start px-4 py-2 text-base border border-dark text-dark'>სტატიის დამატება</Link>
              <Link to="/admin/category" className='self-start px-4 py-2 text-base border border-dark text-dark'>კატეგორიის დამატება</Link>
            </div>

            <div className='border-b border-dark'></div>
              
            <CategoriesManager />

            <div className='border-b border-dark'></div>
            
            <PublisherManager />

            <div className='border-b border-dark'></div>

            <div>
              <h1 className='mb-3 text-lg font-semibold font-firago case-on text-dark'>სტატიები</h1>
              <Articles admin={true} />
            </div>
          </div>
        </section>
    </MainLayout>
  )
}

export default AdminPage
