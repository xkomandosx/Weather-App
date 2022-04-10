import React from "react";
import PropTypes from "prop-types";
import styles from "./CurrentDayDescriptionItem.module.css";

const CurrentDayDescriptionItem = ({ name, value, unit }) => (
  <div className="d-flex justify-content-between">
    <p
      className={`${styles.cardDescItem} mb-0 font-weight-bolder text-uppercase`}
    >
      {name}: {value} {unit}
    </p>
  </div>
);

CurrentDayDescriptionItem.propTypes = {
  name: PropTypes.string.isRequired,
  value: PropTypes.number.isRequired,
  unit: PropTypes.string.isRequired,
};

export default CurrentDayDescriptionItem;
