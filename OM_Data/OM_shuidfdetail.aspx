<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_shuidfdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_shuidfdetail" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
宿舍水电费添加/修改
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">


    <script src="../JS/PickPersons.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
       function fystastic(){
          var diansatrtnum = $("#<%=stratdf.ClientID %>").val();
          var dianendnum=$("#<%=enddf.ClientID %>").val();
          var gongscddfnum=$("#<%=gscddl.ClientID %>").val();
          var dianprice=$("#<%=lbdprice.ClientID %>").html();
          if(diansatrtnum!=""&&dianendnum!=""&&dianprice!="")
          {
              var shiydiandus=parseFloat(dianendnum)-parseFloat(diansatrtnum)-parseFloat(gongscddfnum);
              var shiydianfje=(parseFloat(dianendnum)-parseFloat(diansatrtnum)-parseFloat(gongscddfnum))*parseFloat(dianprice);
              $("#<%=shiydus.ClientID %>").html(shiydiandus.toFixed(2));
              $("#<%=dianfje.ClientID %>").html(shiydianfje.toFixed(2));
          }
          
          var shuisatrtnum = $("#<%=startsf.ClientID %>").val();
          var shuiendnum=$("#<%=endsf.ClientID %>").val();
          var gongscdsfnum=$("#<%=gscdsl.ClientID %>").val();
          var shuiprice=$("#<%=lbsprice.ClientID %>").html();
          if(shuisatrtnum!=""&&shuiendnum!=""&&shuiprice!="")
          {
              var shiyshuiduns=parseFloat(shuiendnum)-parseFloat(shuisatrtnum)-parseFloat(gongscdsfnum);
              var shiyshuifje=(parseFloat(shuiendnum)-parseFloat(shuisatrtnum)-parseFloat(gongscdsfnum))*parseFloat(shuiprice);
              $("#<%=shiyduns.ClientID %>").html(shiyshuiduns.toFixed(2));
              $("#<%=shuifje.ClientID %>").html(shiyshuifje.toFixed(2));
          }
          
          if(shuisatrtnum!=""&&shuiendnum!=""&&shuiprice!=""&&diansatrtnum!=""&&dianendnum!=""&&dianprice!="")
          {
              var renshu=$("#<%=ssrens.ClientID %>").html();
              var shuidianhj=(parseFloat(dianendnum)-parseFloat(diansatrtnum)-parseFloat(gongscddfnum))*parseFloat(dianprice)+(parseFloat(shuiendnum)-parseFloat(shuisatrtnum)-parseFloat(gongscdsfnum))*parseFloat(shuiprice);
              var renjfy=shuidianhj/(parseFloat(renshu));
              $("#<%=shuidianhj.ClientID %>").html(shuidianhj.toFixed(2));
              $("#<%=renjunfy.ClientID %>").html(renjfy.toFixed(2));
          }
       }
    </script>
    
    
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper1">
        <div class="box-outer">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        上一计费周期：<asp:Label ID="lblastdate" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        电费单价：<asp:Label ID="lbdprice" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        水费单价：<asp:Label ID="lbsprice" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center" cellpadding="2" cellspacing="1" border="1" class="toptable grid nowrap">
                <tr>
                    <td>
                        房间号
                    </td>
                    <td>
                        <asp:TextBox ID="ssnum" runat="server" OnTextChanged="ssnum_textchange" AutoPostBack="true" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        现有人数
                    </td>
                    <td>
                        <asp:Label ID="ssrens" runat="server"></asp:Label>
                    </td>
                    <td>
                        起始日期
                    </td>
                    <td>
                        <asp:TextBox ID="startdate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年,dd日" TodaysDateFormat="yyyy年MM月dd日"
                            ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="startdate">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        截止日期
                    </td>
                    <td>
                        <asp:TextBox ID="enddate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年,dd日" TodaysDateFormat="yyyy年MM月dd日"
                            ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="enddate">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        起始电表读数
                    </td>
                    <td>
                        <asp:TextBox ID="stratdf" runat="server" onkeyup="fystastic()"></asp:TextBox>
                    </td>
                    <td>
                        截止电表读数
                    </td>
                    <td>
                        <asp:TextBox ID="enddf" runat="server" onkeyup="fystastic()"></asp:TextBox>
                    </td>
                    <td>
                        公司承担度数
                    </td>
                    <td>
                        <asp:TextBox ID="gscddl" runat="server" onkeyup="fystastic()"></asp:TextBox>
                    </td>
                    <td>
                        个人承担度数
                    </td>
                    <td>
                        <asp:Label ID="shiydus" runat="server"></asp:Label>
                    </td>
                    <td>
                        个人承担电费金额
                    </td>
                    <td>
                        <asp:Label ID="dianfje" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        起始水表读数
                    </td>
                    <td>
                        <asp:TextBox ID="startsf" runat="server" onkeyup="fystastic()"></asp:TextBox>
                    </td>
                    <td>
                        截止水表度数
                    </td>
                    <td>
                        <asp:TextBox ID="endsf" runat="server" onkeyup="fystastic()"></asp:TextBox>
                    </td>
                    <td>
                        公司承担吨数
                    </td>
                    <td>
                        <asp:TextBox ID="gscdsl" runat="server" onkeyup="fystastic()"></asp:TextBox>
                    </td>
                    <td>
                        个人承担吨数
                    </td>
                    <td>
                        <asp:Label ID="shiyduns" runat="server"></asp:Label>
                    </td>
                    <td>
                        个人承担水费金额
                    </td>
                    <td>
                        <asp:Label ID="shuifje" runat="server"></asp:Label>
                    </td>
                 </tr>
                 <tr>
                    <td>
                        个人需承担水电费用合计
                    </td>
                    <td>
                        <asp:Label ID="shuidianhj" runat="server"></asp:Label>
                    </td>
                    <td>
                        人均水电费
                    </td>
                    <td>
                        <asp:Label ID="renjunfy" runat="server"></asp:Label>
                    </td>
                    <td>
                        备注
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="note" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
