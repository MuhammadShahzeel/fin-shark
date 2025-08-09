import type { JSX } from 'react';
import './Card.css'

interface Props {
  companyName: string;
  ticker: string;
  price: number;
}

// props sirf is type k hi hongy or hr card me yehi props pass honge
// companyName, ticker, price
// companyName string hoga, ticker string hoga, price number hoga


const Card: React.FC<Props> = ({
  companyName,
  ticker,
  price,
}: Props): JSX.Element => {
  return (
    <div className="card">
      <img
        src="https://images.unsplash.com/photo-1612428978260-2b9c7df20150?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=580&q=80"
        alt="Image"
      />
      <div className="details">
        <h2>
          {companyName} ({ticker})
        </h2>
        <p>${price}</p>
      </div>
      <p className="info">
        Lorem ipsum dolor, sit amet consectetur adipisicing elit. Magni,
        officia!
      </p>
    </div>
  );
}
export default Card