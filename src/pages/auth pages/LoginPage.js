import React, { useEffect } from 'react';
import MainLayout from '../../components/main layout/MainLayout';
import { useForm } from 'react-hook-form';
import { toast } from '../../components/main layout/Toast';
import { login } from '../../services/apiServices';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { setUserInfo } from '../../store/reducers/userReducers';

const LoginPage = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const userInfo = useSelector(state => state.user.userInfo);
    const { register, handleSubmit, formState: { errors }, reset } = useForm({
        defaultValues: {
            username: '',
            password: '',
        },
    });

    useEffect(() => {
        if(userInfo){
            navigate('/admin');
        }
    }, [userInfo]);

    const onSubmit = async (data) => {
        try {
            const userData = {
                username: data.username,
                password: data.password,
            };

            const responseData = await login(userData);
            dispatch(setUserInfo(responseData));
            localStorage.setItem('admin', JSON.stringify(responseData));
        } catch (error) {
            toast.error('ავტორიზაცია ვერ განხორციელდა: ' + error.message);
        }
    };

    return (
        <MainLayout>
            <section className="px-20 py-10">
                <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col w-1/3 gap-3 mx-auto">
                    <div>
                        <input
                            type="text"
                            placeholder="მომხმარებლის სახელი"
                            {...register('username', { required: 'შეიყვანეთ სახელი' })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.username ? 'border-red-500' : ''}`}
                        />
                        {errors.username && <p className="text-sm text-red-500">{errors.username.message}</p>}
                    </div>
                    <div>
                        <input
                            type="password"
                            placeholder="პაროლი"
                            {...register('password', { 
                                required: 'შეიყვანეთ პაროლი',
                            })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.password ? 'border-red-500' : ''}`}
                        />
                        {errors.password && <p className="text-sm text-red-500">{errors.password.message}</p>}
                    </div>
                    <button type="submit" className="p-2 mt-2 text-newspaper bg-dark">ავტორიზაცია</button>
                </form>
            </section>
        </MainLayout>
    );
};

export default LoginPage;
