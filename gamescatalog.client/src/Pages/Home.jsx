import Filters from '@/Comp/Filters'
import React from 'react'

function Home() {
  return (
    <div className='grid grid-cols-4'>
            <div className='h-full hidden md:block'>
              <Filters />
            </div>
            <div className='col-span-4 md:col-span-3'>Game list</div>
    </div>
  )
}

export default Home