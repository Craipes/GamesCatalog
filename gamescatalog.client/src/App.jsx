import './App.css';
import Header from './Comp/Header';
import Home from "./Pages/Home";
import { useState } from 'react';

function App() {
  const [searchQuery, setSearchQuery] = useState('');

  return (
    <div className='h-screen overflow-hidden'>
      <Header onSearch={setSearchQuery} />
      <Home searchQuery={searchQuery} />
    </div>
  );
}

export default App;
