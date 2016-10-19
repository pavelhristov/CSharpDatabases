<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:os="urn:students">

  <xsl:template match="/">
    <html>
      <head>
        <title>Students</title>
      </head>
      <body>
        <h2>Students</h2>
        <table border="1">
          <thead>
            <tr>
              <th>Name</th>
              <th>Sex</th>
              <th>Birth Date</th>
              <th>Phone</th>
              <th>E-Mail</th>
              <th>Course</th>
              <th>Specialty</th>
              <th>Faculty Number</th>
            </tr>
          </thead>
          <tbody>
            <xsl:for-each select="//os:student">
              <tr>
                <td>
                  <xsl:value-of select="name"/>
                </td>
                <td>
                  <xsl:value-of select="//os:sex"/>
                </td>
                <td>
                  <xsl:value-of select="//os:birthdate"/>
                </td>
                <td>
                  <xsl:value-of select="//os:phone"/>
                </td>
                <td>
                  <xsl:value-of select="//os:email"/>
                </td>
                <td>
                  <xsl:value-of select="//os:course"/>
                </td>
                <td>
                  <xsl:value-of select="//os:specialty"/>
                </td>
                <td>
                  <xsl:value-of select="//os:facultynumber"/>
                </td>
              </tr>
            </xsl:for-each>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
