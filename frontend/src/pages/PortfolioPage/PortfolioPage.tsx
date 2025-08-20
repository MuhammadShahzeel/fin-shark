import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import type { PortfolioGet } from "../../models/Portfolio";
import {
  portfolioGetAPI,
  portfolioDeleteAPI,
} from "../../services/PortfolioService";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio";

const PortfolioPage = () => {
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[] | null>(
    []
  );

  useEffect(() => {
    getPortfolio();
  }, []);

  const getPortfolio = () => {
    portfolioGetAPI()
      .then((res) => {
        if (res?.data) setPortfolioValues(res.data);
      })
      .catch(() => setPortfolioValues(null));
  };

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    portfolioDeleteAPI(e.target[0].value).then((res) => {
      if (res?.status === 200) {
        toast.success("Stock deleted from portfolio!");
        getPortfolio();
      }
    });
  };

  return (
    <div className="max-w-4xl mx-auto p-6">
      <h2 className="text-2xl font-semibold text-gray-800 mb-6 border-b pb-2">
        My Portfolio
      </h2>
      <ListPortfolio
        portfolioValues={portfolioValues!}
        onPortfolioDelete={onPortfolioDelete}
      />
  
    </div>
  );
};

export default PortfolioPage;
