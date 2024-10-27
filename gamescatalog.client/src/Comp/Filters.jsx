import React, { useEffect,useState } from 'react'
import GlobalApi from '../Services/GlobalApi'

function Filters() {

    const [filters, setFilters] = useState([]);
      useEffect(() => {
        getFiltersList();
      }, [])
      const getFiltersList = async () => {
        GlobalApi.getFiltersList.then((response) => {
          console.log(response.data)
          setFilters(response.data)
        })
      }
  
  
    return (
      <div>
        <h2>Каталог</h2>
      </div>
    )
  }
  
  export default Filters