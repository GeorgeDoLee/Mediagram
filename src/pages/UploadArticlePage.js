import React, { useEffect } from 'react';
import MainLayout from '../components/MainLayout';
import useFetch from '../hooks/useFetch';
import { useForm, useFieldArray } from 'react-hook-form';
import { toast } from '../components/Toast';
import { postArticle } from '../services/apiServices';
import { useNavigate } from 'react-router-dom';

const UploadArticlePage = () => {
    const navigate = useNavigate();
    const { data: publishers, isLoading: publishersLoading, error: publishersError } = useFetch('https://localhost:7040/api/Publisher');
    const { data: categories, isLoading: categoriesLoading, error: categoriesError } = useFetch('https://localhost:7040/api/Category');

    const { register, handleSubmit, control, formState: { errors }, reset } = useForm({
        defaultValues: {
            title: '',
            categoryId: '',
            publisherUrls: [{ publisherId: '', url: '' }],
        },
    });
    
    const { fields, append, remove } = useFieldArray({
        control,
        name: 'publisherUrls',
    });

    useEffect(() => {
        if (fields.length === 0) {
            append({ publisherId: '', url: '' });
        }
    }, [append, fields.length]);

    const onSubmit = async (data) => {
        try {
            const publisherUrls = data.publisherUrls.reduce((acc, { publisherId, url }) => {
                acc[publisherId] = url;
                return acc;
            }, {});

            const articleData = {
                title: data.title,
                categoryId: data.categoryId,
                publisherUrls,
                photo: null,
            };

            await postArticle(articleData);
            toast.success('სტატია აიტვირთა წარმატებით');
            setTimeout(() => {
                navigate('/admin');
            }, 1000);
        } catch (error) {
            toast.error(error.message);
        }
    };

    return (
        <MainLayout>
            <section className="px-20 py-5">
                <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col w-1/2 gap-3 mx-auto">
                    <div>
                        <input
                            type="text"
                            placeholder="სათაური"
                            {...register('title', { required: 'შეიყვანეთ სათაური' })}
                            className={`w-full bg-transparent p-2 border border-dark outline-none ${errors.title ? 'border-red-500' : ''}`}
                        />
                        {errors.title && <p className="text-sm text-red-500">{errors.title.message}</p>}
                    </div>
                    <div>
                        <select
                            {...register('categoryId', { required: 'აირჩიეთ კატეგორია' })}
                            className="w-full p-2 bg-transparent border border-dark"
                        >
                            <option value="" disabled>აირჩიეთ კატეგორია</option>
                            {categories && categories.map(category => (
                                <option key={category.id} value={category.id}>{category.name}</option>
                            ))}
                        </select>
                        {errors.categoryId && <p className="text-sm text-red-500">{errors.categoryId.message}</p>}
                    </div>
                    <div className="flex flex-col gap-3">
                        {fields.map((item, index) => (
                            <div key={item.id} className="flex gap-3">
                                <select
                                    {...register(`publisherUrls.${index}.publisherId`, { required: 'აირჩიეთ მედია' })}
                                    className="w-1/3 p-2 bg-transparent border border-dark"
                                >
                                    <option value="" disabled>აირჩიეთ მედია</option>
                                    {publishers && publishers.map(publisher => (
                                        <option key={publisher.id} value={publisher.id}>{publisher.name}</option>
                                    ))}
                                </select>
                                <input
                                    type="url"
                                    placeholder="ლინკი"
                                    {...register(`publisherUrls.${index}.url`, { required: 'შეიყვანეთ ლინკი' })}
                                    className="w-2/3 p-2 bg-transparent border outline-none border-dark"
                                />
                                <button
                                    type="button"
                                    onClick={() => remove(index)}
                                    className="p-2 text-white bg-red-500"
                                >
                                    წაშლა
                                </button>
                                {errors.publisherUrls?.[index]?.url && (
                                    <p className="text-sm text-red-500">{errors.publisherUrls[index].url.message}</p>
                                )}
                            </div>
                        ))}
                        <button
                            type="button"
                            onClick={() => append({ publisherId: '', url: '' })}
                            className="p-2 text-white bg-blue-500"
                        >
                            დამატება
                        </button>
                    </div>
                    <button type="submit" className="p-2 mt-2 text-newspaper bg-dark">სტატიის ატვირთვა</button>
                </form>
            </section>
        </MainLayout>
    );
};

export default UploadArticlePage;
