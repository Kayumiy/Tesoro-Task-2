using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using UnityEngine;

public class XmlReader 
{
    public static string XmlGetOne(string xmlStream, string xPath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode xmlNode;

        try
        {
            xmlDoc.LoadXml(xmlStream);
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.ToString());

            return null;
        }

        try
        {
            xmlNode = xmlDoc.SelectSingleNode(xPath);

            return xmlNode.InnerText;
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.ToString());

            return null;
        }
    }

    public static StringCollection XmlGetMany(string xmlStream, string xPath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        StringCollection strCollection = new StringCollection();


        try
        {
            xmlDoc.LoadXml(xmlStream);
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.ToString());

            return null;
        }

        try
        {

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement.SelectNodes(xPath))
            {
                strCollection.Add(xmlNode.InnerText);
            }

            return strCollection;
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.ToString());

            return null;
        }
    }

}
