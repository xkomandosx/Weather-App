import React, { Fragment, useState, useEffect } from "react";

import Header from "../Header";
import Form from "../Form";
import Error from "../Error";
import Loader from "../Loader";
import Forecast from "../Forecast";

import useForecast from "../../hooks/useForecast";

import styles from "./Page.module.css";

const Page = () => {
  const [locations, setLocations] = useState([]);
  const { isError, isLoading, forecast, submitRequest, getAllLocations } =
    useForecast();

  useEffect(() => {
    getAllLocations().then((response) => setLocations(response));
  }, []);

  const onSubmit = (value) => {
    submitRequest(value);
  };

  return (
    <Fragment>
      <Header />
      {!forecast && (
        <div className={`${styles.box} position-relative`}>
          {/* Form */}
          {!isLoading && <Form submitSearch={onSubmit} locations={locations} />}
          {/* Error */}
          {isError && <Error message={isError} />}
          {/* Loader */}
          {isLoading && <Loader />}
        </div>
      )}
      {/* Forecast */}
      {forecast && <Forecast forecast={forecast} />}
    </Fragment>
  );
};

export default Page;
