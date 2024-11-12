import React, { useState, useEffect } from 'react';

const useCoverageCalculator = (article) => {
    const [mostCoverage, setMostCoverage] = useState({ percentage: 0, position: '' });

    useEffect(() => {
        const { govCoverage, centerCoverage, oppCoverage } = article;

        if (govCoverage >= centerCoverage && govCoverage >= oppCoverage) {
            setMostCoverage({ percentage: govCoverage, position: 'სამთავრობო' });
        } else if (oppCoverage >= centerCoverage && oppCoverage >= govCoverage) {
            setMostCoverage({ percentage: oppCoverage, position: 'ოპოზიციური' });
        } else {
            setMostCoverage({ percentage: centerCoverage, position: 'ცენტრისტული' });
        }
    }, [article]);

    return mostCoverage;
}

export default useCoverageCalculator;