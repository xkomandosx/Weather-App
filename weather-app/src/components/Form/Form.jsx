import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { Hint } from "react-autocomplete-hint";
import styles from "./Form.module.css";

const Form = ({ submitSearch, locations }) => {
  const [location, setLocation] = useState("");
  const [hintData, setHintData] = useState([]);

  useEffect(() => {
    setHintData(
      locations.map(function (a) {
        return a.title;
      })
    );
  }, []);

  const onSubmit = (e) => {
    e.preventDefault();
    if (!location || location === "") return;
    submitSearch(location);
  };

  return (
    <form className="form" onSubmit={onSubmit}>
      <Hint options={hintData} allowTabFill>
        <input
          aria-label="location"
          type="text"
          className={`${styles.form__field}`}
          placeholder="Search for location"
          required
          value={location}
          onChange={(e) => setLocation(e.target.value)}
        />
      </Hint>
      <button
        type="submit"
        className={`${styles.btn} btn--primary btn--inside uppercase`}
        onClick={onSubmit}
      >
        SEARCH
      </button>
    </form>
  );
};

Form.propTypes = {
  submitSearch: PropTypes.func.isRequired,
};

export default Form;
