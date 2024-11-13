import React from 'react';
import MainLayout from '../../components/main layout/MainLayout';
import { useForm } from 'react-hook-form';
import { toast } from '../../components/main layout/Toast';
import { registerAdmin } from '../../services/apiServices';
import { useNavigate } from 'react-router-dom';

const RegistrationPage = () => {
    const navigate = useNavigate();
    const { register, handleSubmit, formState: { errors }, watch, reset } = useForm({
        defaultValues: {
            username: '',
            password: '',
            repeatPassword: '',
        },
    });

    const onSubmit = async (data) => {
        try {
            const userData = {
                username: data.username,
                password: data.password,
            };

            await registerAdmin(userData);
            toast.success('რეგისტრაცია წარმატებით დასრულდა');
            setTimeout(() => {
                navigate('/login');
            }, 1000);
        } catch (error) {
            toast.error(error.message);
        }
    };

    const password = watch('password');

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
                                minLength: {
                                    value: 8,
                                    message: 'პაროლი უნდა შედგებოდეს მინიმუმ 8 სიმბოლოსგან',
                                }
                            })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.password ? 'border-red-500' : ''}`}
                        />
                        {errors.password && <p className="text-sm text-red-500">{errors.password.message}</p>}
                    </div>
                    <div>
                        <input
                            type="password"
                            placeholder="გაიმეორეთ პაროლი"
                            {...register('repeatPassword', {
                                required: 'გაიმეორეთ პაროლი',
                                validate: value => value === password || 'პაროლები არ ემთხვევა',
                            })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.repeatPassword ? 'border-red-500' : ''}`}
                        />
                        {errors.repeatPassword && <p className="text-sm text-red-500">{errors.repeatPassword.message}</p>}
                    </div>
                    <button type="submit" className="p-2 mt-2 text-newspaper bg-dark">რეგისტრაცია</button>
                </form>
            </section>
        </MainLayout>
    );
};

export default RegistrationPage;
