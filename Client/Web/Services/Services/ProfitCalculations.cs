﻿using CapitalClue.Frontend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalClue.Frontend.Web.Services.Services;

public class ProfitCalculations
{
    private readonly PropertyPredictionDto _propertyPredictionDto;
    private readonly StockPredictionDto _stockPredictionDto;
    private readonly PropertyPurchaseInfo _propertyPurchaseInfo;

    public ProfitCalculations(PropertyPredictionDto propertyPredictionDto, StockPredictionDto stockPredictionDto, PropertyPurchaseInfo propertyPurchaseInfo)
    {
        _propertyPredictionDto = propertyPredictionDto;
        _stockPredictionDto = stockPredictionDto;
        _propertyPurchaseInfo = propertyPurchaseInfo;
    }

    public PropertyPredictionResult PropertyPrediction()
    {
        PropertyPredictionResult propertyPredictionResult = new PropertyPredictionResult();

        var firstPrice = _propertyPurchaseInfo.PropertyPrice;
        foreach (var item in _propertyPredictionDto.ForeCastIndex)
        {
            firstPrice += (firstPrice * (1 + item.Value));
            propertyPredictionResult.ForeCastIndex.Add(item.Key, firstPrice);
        }
        propertyPredictionResult.Gain = firstPrice - _propertyPurchaseInfo.PropertyPrice;

        firstPrice = _propertyPurchaseInfo.PropertyPrice;
        foreach (var item in _propertyPredictionDto.ConfidenceLowerBound)
        {
            firstPrice += (firstPrice * (1 + item.Value));
            propertyPredictionResult.ConfidenceLowerBound.Add(item.Key, firstPrice);
        }
        propertyPredictionResult.ConfidenceLowerGain = firstPrice - _propertyPurchaseInfo.PropertyPrice;

        firstPrice = _propertyPurchaseInfo.PropertyPrice;
        foreach (var item in _propertyPredictionDto.ConfidenceUpperBound)
        {
            firstPrice += (firstPrice * (1 + item.Value));
            propertyPredictionResult.ConfidenceUpperBound.Add(item.Key, firstPrice);
        }
        propertyPredictionResult.ConfidenceUpperGain = firstPrice - _propertyPurchaseInfo.PropertyPrice;

        return propertyPredictionResult;
    }

    public StockPredictionResult StockPrediction(float downpayment, float monthlyContribute)
    {
        StockPredictionResult stockPredictionResult = new StockPredictionResult();
        var anualContribute = monthlyContribute * 12;

        var firstPrice = downpayment;
        foreach (var item in _stockPredictionDto.ForeCastIndex)
        {
            firstPrice += firstPrice * (1 + item.Value);
            stockPredictionResult.ForeCastIndex.Add(item.Key, firstPrice);
            firstPrice += anualContribute;
        }
        stockPredictionResult.Gain = firstPrice - downpayment - anualContribute;

        firstPrice = downpayment;
        foreach (var item in _stockPredictionDto.ConfidenceLowerBound)
        {
            firstPrice += (firstPrice * (1 + item.Value));
            stockPredictionResult.ConfidenceLowerBound.Add(item.Key, firstPrice);
            firstPrice += anualContribute;
        }
        stockPredictionResult.ConfidenceLowerGain = firstPrice - downpayment - anualContribute;

        firstPrice = downpayment;
        foreach (var item in _stockPredictionDto.ConfidenceUpperBound)
        {
            firstPrice += (firstPrice * (1 + item.Value));
            stockPredictionResult.ConfidenceUpperBound.Add(item.Key, firstPrice);
            firstPrice += anualContribute;
        }
        stockPredictionResult.ConfidenceUpperGain = firstPrice - downpayment - anualContribute;

        return stockPredictionResult;
    }

    public (PropertyPredictionResult, StockPredictionResult) CompareHousePurchaseAndStock(float monthlyRent)
    {
        StockPredictionResult stockPredictionResult = new StockPredictionResult();
        PropertyPredictionResult propertyPredictionResult = new PropertyPredictionResult();

        //If the user purchased house
        propertyPredictionResult = PropertyPrediction();

        //if the user rented a house and invested
        var monthlyContribution = _propertyPurchaseInfo.MortgageMonthlyPayment - monthlyRent;
        stockPredictionResult = StockPrediction(_propertyPurchaseInfo.DownPayment, monthlyContribution);

        return (propertyPredictionResult, stockPredictionResult);
    }
}