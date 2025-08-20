import type { SyntheticEvent } from "react";
import CardPortfolio from "../CardPortfolio/CardPortfolio";
import type { PortfolioGet } from "../../../models/Portfolio";



interface Props {
  portfolioValues: PortfolioGet[];
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

const ListPortfolio = ({ portfolioValues, onPortfolioDelete }: Props) => {
  return (
    <section id="portfolio">
 
      <div className="relative flex flex-col items-center max-w-5xl mx-auto space-y-10 px-10 mb-5 md:px-6 md:space-y-0 md:space-x-7 md:flex-row">
        <>
          {portfolioValues.length > 0 ? (
            portfolioValues.map((portfolioValue) => {
              return (
                <CardPortfolio
                  key={portfolioValue.id}
                  portfolioValue={portfolioValue}
                  onPortfolioDelete={onPortfolioDelete}
                />
              );
            })
          ) : (
          <p className="text-gray-500 italic">Your portfolio is empty.</p>
          )}
        </>
      </div>
    </section>
  );
};

export default ListPortfolio;