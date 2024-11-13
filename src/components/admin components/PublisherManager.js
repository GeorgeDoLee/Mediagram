import React from 'react'
import { Link } from 'react-router-dom';
import { toast } from '../main layout/Toast';
import { deletePublisher } from '../../services/apiServices';
import useFetch from '../../hooks/useFetch';
import { useSelector } from 'react-redux';

const PublisherManager = () => {
    const userInfo = useSelector(state => state.user.userInfo);
    const {data: publishers, isLoading, error, refetch} = useFetch('https://localhost:7040/api/Publisher');

    const handleDelete = async (id, name) => {
        if(window.confirm(`ნამდვილად გსურთ წაშალოთ ${name}?`)){
            try {
                await deletePublisher(id, userInfo.token);
                toast.success('მედია წაიშალა წარმატებით')
                refetch();
            } catch (error) {
                toast.error('მოხდა შეცდომა')
            }
        }
    }

  return (
        <div className='w-full'> 
            <h1 className='mb-2 text-lg font-semibold font-firago case-on text-dark'>მედია</h1>
            <div className='flex flex-wrap gap-2'>
                {isLoading && <p>იტვირთება...</p>}
                {error && <p>{error.message}</p>}
                {publishers && publishers.map((publisher) => (
                    <div
                        key={publisher.id}
                        className='flex flex-col gap-2 p-2 border w-fit border-dark'
                    >
                        <div className='flex items-center gap-2'>
                            {publisher.logo ? 
                                <img 
                                    src={publisher.logo}
                                    alt={publisher.name} 
                                    className="object-contain w-12 h-12 rounded-full"
                                /> 
                                :
                                <div className='w-12 h-12 rounded-full bg-dark' />
                            }
                            <h1 className='text-lg text-dark font-firago'>{publisher.name}</h1>
                        </div>

                        <div className='border-b border-dark' />
                        
                        <div className='flex justify-between gap-10 font-firago text-dark'>
                            <Link to={`/admin/publisher/${publisher.id}`} className='text-xs'>განახლება</Link>
                            <button onClick={() => handleDelete(publisher.id, publisher.name)} className='text-xs text-red-500'>წაშლა</button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
  )
}

export default PublisherManager
