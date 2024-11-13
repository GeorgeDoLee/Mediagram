import React, { useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import MainLayout from '../../components/main layout/MainLayout'
import useFetch from '../../hooks/useFetch';
import { postCategory, putCategory } from '../../services/apiServices';
import { toast } from '../../components/main layout/Toast';
import { useSelector } from 'react-redux';

const UploadCategoryPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const userInfo = useSelector(state => state.user.userInfo);
    const { data: category, isLoading, error } = useFetch(id ? `https://localhost:7040/api/Category/${id}` : null);

    const { register, handleSubmit, reset, formState: { errors } } = useForm({
        defaultValues: {
            name: category ? category.name : '',
        }
    });

    useEffect(() => {
        if (category) {
            reset({
                name: category.name,
            });
        }
    }, [category, reset]);

    const onSubmit = async (data) => {
        try {
            if (id) {
                await putCategory(id, data, userInfo.token);
                toast.success('კატეგორია განახლდა წარმატებით');
            } else {
                await postCategory(data, userInfo.token);
                toast.success('კატეგორია დაემატა წარმატებით');
            }
            setTimeout(() => {
                navigate('/admin');
            }, 1000);
        } catch (error) {
            toast.error(error.message);
        }
    };

    return (
        <MainLayout>
            <section className='px-20 py-10'>
                <form onSubmit={handleSubmit(onSubmit)} className='flex flex-col w-1/2 gap-3 mx-auto'>
                    <div>
                        <input
                            type="text"
                            placeholder="სახელი"
                            {...register('name', { required: 'მიუთითეთ კატეგორიის სახელი' })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.name ? 'border-red-500' : ''}`}
                        />
                        {errors.name && <p className="text-sm text-red-500">{errors.name.message}</p>}
                    </div>
                    <button type="submit" className="p-2 text-newspaper bg-dark">ატვირთვა</button>
                </form>
            </section>
        </MainLayout>
    );
};

export default UploadCategoryPage;
