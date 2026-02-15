import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import axios from 'axios'

function App() {
 const [products,setProducts]=useState([])
 const[errorHandle,setErrorHandle]=useState(false)
 useEffect(()=>{
  axios.get("https://localhost:7273/api/Product/GetProducts")
  .then((res)=>{
    console.log(res)
    if(res.status===200)
    {
      setProducts(res.data)
    }
  })
  .catch((e)=>{
    if(e.response.data.status===404){
      setErrorHandle(true)
    }
    console.log(e);
  })
 },[])

  return (
    <>
     {errorHandle ?"Bir hata oluştu":
      products.map((product,key)=>
      <div key={key}>
        <h3>{product.title}</h3>
        <p>{product.description}</p>
        <p>{product.price}</p>
      </div>
      )
     } 
    </>
  )
}

export default App
