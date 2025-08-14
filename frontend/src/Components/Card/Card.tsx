import type { JSX } from 'react';
import './Card.css'
import type { CompanySearch } from '../../company';
import React, { type SyntheticEvent } from "react";
import AddPortfolio from '../Portfolio/AddPortfolio/AddPortfolio';

interface Props {
  id: string;
  searchResult: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
  
}

// props sirf is type k hi hongy or hr card me yehi props pass honge
// companyName, ticker, price
// companyName string hoga, ticker string hoga, price number hoga


const Card: React.FC<Props> = ({
  id,
  searchResult,
  onPortfolioCreate,
}: Props): JSX.Element => {
  return (
    <div key={id} id={id} className="card">
      <div className="details">
        <h2>
          {searchResult.name} ({searchResult.symbol})
        </h2>
        <p>{searchResult.currency}</p>
      </div>
      <p className="info">
        {searchResult.exchangeShortName} - {searchResult.stockExchange}
      </p>
        <AddPortfolio
        onPortfolioCreate={onPortfolioCreate}
        symbol={searchResult.symbol}
      />
    </div>
  );
}
export default Card