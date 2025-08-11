import React, { useState, type ChangeEvent, type JSX, type SyntheticEvent } from 'react'

type Props = {}

const Search: React.FC<Props> = (props: Props): JSX.Element => {
  const [search, setSearch] = useState<string>("");
//   Yahan <string> ka matlab:
// search ka type string hoga.
// setSearch sirf string accept karega.

  const handleChange = (e:ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(e);
  };
  const onClick = (e:SyntheticEvent) => {
    console.log(e);
  }
  return (
    <div>
      <input value={search} onChange={handleChange}></input>
      <button onClick={onClick}>Search</button>
    </div>
  );
};

export default Search