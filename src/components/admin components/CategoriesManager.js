import React from 'react';
import { toast } from '../main layout/Toast';
import { deleteCategory } from '../../services/apiServices';
import { Link } from 'react-router-dom';
import useFetch from '../../hooks/useFetch';
import { useSelector } from 'react-redux';

const CategoriesManager = () => {
    const userInfo = useSelector(state => state.user.userInfo);
    const { data: categories, isLoading, error, refetch } = useFetch('https://localhost:7040/api/Category');

    const handleDelete = async (id, name) => {
        if (window.confirm(`ნამდვილად გსურთ წაშალოთ კატეგორია "${name}"?`)) {
            try {
                console.log(userInfo.token);
                await deleteCategory(id, userInfo.token);
                toast.success('კატეგორია წაიშალა წარმატებით');
                refetch();
            } catch (error) {
                toast.error('მოხდა შეცდომა');
            }
        }
    };

    return (
        <div className='w-full'> 
            <h1 className='mb-2 text-lg font-semibold font-firago case-on text-dark'>კატეგორიები</h1>
            <div className='flex flex-wrap gap-3'>
                {isLoading && <p>იტვირთება...</p>}
                {error && <p>{error.message}</p>}
                {categories && categories.map((category) => (
                    <div 
                        key={category.id}
                        className='p-2 border border-dark text-dark font-firago'
                    >
                        <h1 className='text-lg'>
                            {category.name}
                        </h1>
                        <div className='flex gap-3 mt-2'>
                            <Link to={`/admin/category/${category.id}`} className='text-xs'>რედაქტირება</Link>
                            <button
                                onClick={() => handleDelete(category.id, category.name)}
                                className='text-xs text-red-500'
                            >
                                წაშლა
                            </button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default CategoriesManager;
