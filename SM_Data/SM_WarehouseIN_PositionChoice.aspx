<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseIN_PositionChoice.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIN_PositionChoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <base target="download" />
    <title>选择仓位</title>
</head>
<body style="background-color:#CCCCFF; width: 950px;">
<script type="text/javascript" language="javascript">
    function confirm()
    {
        var rbl = document.getElementById("RadioButtonListWL");
        var retval = new Array(2);
        retval[0] = "0";
        retval[1] = "待查";        
        for (i = 0; i < rbl.childNodes.length; i++) 
        {
            if (rbl.childNodes[i].checked == true)
            {
                retval[0] = rbl.childNodes[i].value 
                retval[1] = rbl.childNodes[i+1].innerHTML;
                window.returnValue =  retval;
                window.close();
//                break;
             }
         }
         window.returnValue = retval;
         window.close();
    }

    function goback()
     {
//        var retval = new Array(2);
//        retval[0] = "0";
//        retval[1] = "待查";
//        window.returnValue = retval;  
        window.close();
    }
</script>
    <form id="form1" runat="server" >
    
      <iframe id="download" name="download" height="0px" width="0px"></iframe>
    
    <div style="width: 850px; height: auto; margin-right: auto; margin-left: auto; text-align: center;">
    <table >
        
        <tr>
            <td style="font-size:x-large; font-weight: bold;">请选择--<asp:Label ID="LabelWS" runat="server"></asp:Label>--仓位</td>
        </tr>
        <tr>
            <td style=" font-weight: bold;">历史仓位</td>
        </tr>
        <tr>
            <td style="text-align: center">
            <asp:RadioButtonList ID="RadioButtonListWL" runat="server"  RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="Table" CellPadding="2" CellSpacing="1" 
              OnSelectedIndexChanged="RadioButtonListWL_SelecedIndexChanged" AutoPostBack="True" TextAlign="Right" >
            </asp:RadioButtonList>
            </td>       
        </tr>
        <tr>
            <td style="font-weight: bold;">全部仓位</td>
        </tr>
         <tr>
            <td style="text-align: left">
            <asp:RadioButtonList ID="RadioButtonListAllWL" runat="server"  RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="Table" CellPadding="2" CellSpacing="1" 
              OnSelectedIndexChanged="RadioButtonListWL_SelecedIndexChanged" AutoPostBack="True" TextAlign="Right" >
            </asp:RadioButtonList>
            </td>       
        </tr>
        
    </table>
    </div>
    </form>
</body>
</html>
