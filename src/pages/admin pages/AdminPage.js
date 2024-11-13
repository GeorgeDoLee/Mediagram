import React, { useEffect } from 'react'
import MainLayout from '../../components/main layout/MainLayout'
import PublisherManager from '../../components/admin components/PublisherManager'
import CategoriesManager from '../../components/admin components/CategoriesManager';
import Articles from '../../components/Articles'
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { resetUserInfo } from '../../store/reducers/userReducers';

const AdminPage = () => {
  const userInfo = useSelector(state => state.user.userInfo);
  const dispatch = useDispatch();
  const navigate = useNavigate();
    
  useEffect(() => {
    if(userInfo === null){
      navigate('/login')
    }
  }, [userInfo])

  const logoutHandler = () => {
    dispatch(resetUserInfo());
    localStorage.removeItem('admin');
  } 
  
  return (
    <MainLayout>
        <section className='px-20 py-10'>
          <div className='flex flex-col gap-5'>
            <div className='flex justify-between font-firago'>
              <div className='flex gap-2'>
                <Link to="/admin/publisher" className='self-start px-4 py-2 text-base border border-dark text-dark'>მედიის დამატება</Link>
                <Link to="/admin/article" className='self-start px-4 py-2 text-base border border-dark text-dark'>სტატიის დამატება</Link>
                <Link to="/admin/category" className='self-start px-4 py-2 text-base border border-dark text-dark'>კატეგორიის დამატება</Link>
              </div>
              <button onClick={() => logoutHandler()} className='px-4 py-2 text-base bg-dark text-neutral-50'>გამოსვლა</button>
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
