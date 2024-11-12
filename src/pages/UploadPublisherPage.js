import React, { useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import MainLayout from '../components/MainLayout';
import useFetch from '../hooks/useFetch';
import { postPublisher, putPublisher } from '../services/apiServices';
import { toast } from '../components/Toast';

const UploadPublisherPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const { data: publisher, isLoading, error } = useFetch(id ? `https://localhost:7040/api/Publisher/${id}` : null);

    const { register, handleSubmit, reset, formState: { errors } } = useForm({
        defaultValues: {
            name: publisher ? publisher.name : '',
            titleClass: publisher ? publisher.titleClass : '',
            articleClass: publisher ? publisher.articleClass : '',
            position: publisher? publisher.position : '',
            logo: ''
        }
    });

    useEffect(() => {
        if (publisher) {
            reset({
                name: publisher.name,
                titleClass: publisher.titleClass,
                articleClass: publisher.articleClass,
                position: publisher.position,
                logo: ''
            });
        }
    }, [publisher, reset]);

    const onSubmit = async (data) => {
        try {
            if(id){
                await putPublisher(id, data);
                toast.success('მედია განახლდა წარმატებით')
            } else {
                await postPublisher(data);
                toast.success('მედია დაემატა წარმატებით')
            }
            setTimeout(() => {
                navigate('/admin')
            }, 1000);
        } catch (error) {
            toast.error(error.message)
        }
    };

    return (
        <MainLayout>
            <section className='px-20 py-5'>
                <form onSubmit={handleSubmit(onSubmit)} className='flex flex-col w-1/2 gap-3 mx-auto'>
                    <div>
                        <input
                            type="text"
                            placeholder="სახელი"
                            {...register('name', { required: 'მიუთითეთ სახელი' })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.name ? 'border-red-500' : ''}`}
                        />
                        {errors.name && <p className="text-sm text-red-500">{errors.name.message}</p>}
                    </div>
                    <div>
                        <input
                            type="text"
                            placeholder="სათაურის კლასი"
                            {...register('titleClass', { required: 'მიუთითეთ სათაურის კლასი' })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.titleClass ? 'border-red-500' : ''}`}
                        />
                        {errors.titleClass && <p className="text-sm text-red-500">{errors.titleClass.message}</p>}
                    </div>
                    <div>
                        <input
                            type="text"
                            placeholder="სტატიის კლასი"
                            {...register('articleClass', { required: 'მიუთითეთ სტატიის კლასი' })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.articleClass ? 'border-red-500' : ''}`}
                        />
                        {errors.articleClass && <p className="text-sm text-red-500">{errors.articleClass.message}</p>}
                    </div>
                    <div>
                        <select 
                            {...register('position', { required: 'მიუთითეთ პოზიცია' })} 
                            className={`w-full bg-transparent p-2 border border-dark ${errors.position ? 'border-red-500' : ''}`}
                        >
                            <option value="" disabled>პოზიცია</option>
                            <option value="gov">სამთავრობო</option>
                            <option value="center">ცენტრისტული</option>
                            <option value="opp">ოპოზიციური</option>
                        </select>
                        {errors.position && <p className="text-sm text-red-500">{errors.position.message}</p>}
                    </div>
                    <button type="submit" className="p-2 text-newspaper bg-dark">ატვირთვა</button>
                </form>
            </section>
        </MainLayout>
    );
}

export default UploadPublisherPage;
