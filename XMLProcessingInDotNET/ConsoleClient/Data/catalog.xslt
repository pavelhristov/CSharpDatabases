<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="@* | node()">
    <html>
      <head>
        <title>Catalog</title>
      </head>
      <body>
        <xsl:for-each select="album">
          <ul>
            <li>
              Name: <xsl:value-of select="name"/>
            </li>
            <li>
              Artists: <xsl:value-of select="artist"/>
            </li>
            <li>
              Producer: <xsl:value-of select="producer"/>
            </li>
            <li>
              Price: <xsl:value-of select="price"/>
            </li>
            <li>
              Songs:
              <ul>
                <xsl:for-each select="songs/song">
                  <li>
                    <xsl:value-of select="title"/> - <xsl:value-of select="duration"/>
                  </li>
                </xsl:for-each>
              </ul>
            </li>
          </ul>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
