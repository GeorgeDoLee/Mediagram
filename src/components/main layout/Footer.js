import React from 'react'
import Logo from '../Logo'

const List = ({title, list}) => {
    return (
        <div>
            <h1 className='mb-3 text-lg font-semibold case-on'>{title}</h1>
            <ul className='flex flex-col gap-1'>
                {list && list.map((item, index) => (
                    <li key={index} className='text-base'>{item}</li>
                ))}
            </ul>
        </div>
    )
}

const Footer = () => {
  return (
    <section className='bg-dark'>
        <div className='flex flex-col px-20 py-14 gap-14'>
            <div className='flex flex-col gap-10 text-white lg:flex-row lg:gap-52 font-firago'>
                <div className='flex lg:block'>
                    <Logo />
                </div>
                <div className='flex flex-col gap-10 lg:flex-row lg:gap-52'>
                    <List title='კომპანია' list={['ჩვენს შესახებ', 'ისტორია', 'მისია']} />
                    <List title='დახმარება' list={['კონტაქტი', 'ხშირად დასმული კითხვები', 'დახმარების ცენტრი']} />
                    <List title='ნავიგაცია' list={['მთავარი', 'ახალი ამბები', 'ბრმა წერტილები']} />
                </div>
            </div>

            <div className='border-b border-white' />

            <div className='flex flex-col items-center gap-10 lg:justify-between lg:flex-row'>
                <ul className='flex gap-5 text-xs text-white lg:gap-10 font-firago case-on lg:text-base'>
                    <li>
                        დონაცია
                    </li>
                    <li>
                        უსაფრთხოების პოლიტიკა
                    </li>
                    <li>
                        წესები და პირობები 
                    </li>
                </ul>
                
                <p className='text-sm text-white lg:text-base font-firago case-on'>
                    &copy; გიორგი დოლიძის ციფრული საკუთრება
                </p>
            </div>
        </div>
    </section>
  )
}

export default Footer
