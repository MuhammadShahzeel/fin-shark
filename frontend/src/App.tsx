

import { useState, type ChangeEvent, type SyntheticEvent } from 'react';
import CardList from './Components/CardList/CardList'
import Search from './Components/Search/Search'
import { searchCompanies, type SearchResponse } from './api';
import type { CompanySearch } from './company';

function App() {
  const [search, setSearch] = useState<string>("");
const [searchResult, setSearchResult] = useState<CompanySearch[]>();
  const [serverError, setServerError] = useState<string | null>(null);

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(e);
  };

  const onClick = async (e: SyntheticEvent) => {
    const result = await searchCompanies(search);
    //setServerError(result.data);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
       console.log(result.data);
    }
   
  };
  

  return (
    <>
    <div className="App"> 
       <Search onClick={onClick} search={search} handleChange={handleChange} />
 <CardList />
    </div>
     
        
    </>
  )
}

export default App

function setServerError(result: string) {
  throw new Error('Function not implemented.');
}

function setSearchResult(data: SearchResponse & any[]) {
  throw new Error('Function not implemented.');
}

