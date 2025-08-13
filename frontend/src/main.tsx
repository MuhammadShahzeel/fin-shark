import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { searchCompanies } from './api.tsx';

searchCompanies("tsla").then((res) => {
  console.log(res); // Axios ka response object ka .data
});


createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
