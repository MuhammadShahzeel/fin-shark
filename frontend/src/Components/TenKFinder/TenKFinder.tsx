import React, { useEffect, useState } from "react";
import { v4 as uuidv4 } from "uuid";

import { getTenK } from "../../api";

import Spinner from "../Spinners/Spinner";
import type { CompanyTenK } from "../../company";
import TenKFinderItem from "../TenKFinderItem/TenKFinderItem";

type Props = {
  ticker: string;
};

const TenKFinder = ({ ticker }: Props) => {
  const [companyData, setCompanyData] = useState<CompanyTenK[]>();
  useEffect(() => {
    const getTenKData = async () => {
      const value = await getTenK(ticker);
      setCompanyData(value?.data);
    };
    getTenKData();
  }, [ticker]);
  return (
    <div className="inline-flex rounded-md shadow-sm m-4" role="group">
      {companyData ? (
        companyData?.slice(0, 5).map((tenK) => {
          return <TenKFinderItem tenK={tenK} key={uuidv4()} />;
        })
      ) : (
        <Spinner />
      )}
    </div>
  );
};

export default TenKFinder;