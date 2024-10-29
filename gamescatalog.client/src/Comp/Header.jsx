import React from 'react';
import logo from './../assets/Images/logo.png';
import { HiOutlineMagnifyingGlass } from 'react-icons/hi2';

function Header({ onSearch }) {
  const handleSearch = (e) => {
    onSearch(e.target.value);
  };

  return (
    <div className='flex items-center p-3'>
      <img src={logo} width={60} height={60} alt="Logo" />
      <div className='flex bg-slate-200 p-2 w-full mx-5 rounded-full items-center'>
        <HiOutlineMagnifyingGlass />
        <input
          type="text"
          placeholder="Пошук"
          className='px-2 bg-transparent outline-none'
          onChange={handleSearch}
        />
      </div>
    </div>
  );
}

export default Header;
