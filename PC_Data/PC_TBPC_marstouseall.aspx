<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_marstouseall.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_marstouseall"
    Title="物料库存占用" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="commanrepeater.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
      
    function bg(temp)
    {
       var i=window.showModalDialog("PC_TBPC_Purchange_all_detail.aspx?maiinfo="+temp,'',"dialogHeight:600px;dialogWidth:1200px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no"); 
    }
   function String.prototype.Trim()   
   {
         return this.replace(/\s+/g,"");
   }  
     function setto(no)
    {
        var table=document.getElementById("Table1"); 
        var tr=table.getElementsByTagName("tr"); 
        var pattem=/^\d+(\.\d+)?$/; 
        var a1;
        var a2;
        var b;
        var sum1=0;
        var sum2=0;
        var s, ss;
        function getid(i)
        {
            alert("3")
            alert(repeaterId + "_ctl" + getrownumber(i) + "_USENUM");
            return "'"+repeaterId + "_ctl" + getrownumber(i) + "_USENUM"+"'";
            
        }
        function getidd(i)
        {
            return "'"+repeaterId + "_ctl" + getrownumber(i) + "tb_kuntotal2"+"'";
        }
           
            function getrownumber(i) 
            {
                if (i > 10) {
                    return i;
                }
                else 
                {
                    return '0' + i;
                }
            }
            switch(no)
            {
                case 0:
                {
                   for( i=1;i <tr.length-2;i++) 
                   {
                        b=parseFloat(tr[i].getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML);
                        s=tr[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                        ss=s.substring(0, 5); 
                        if(ss!="01.07")
                        {
                            if(pattem.test(tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value.Trim();
                                a1=parseFloat(tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value); 
                                if(a1<0)
                                {
                                    tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=-tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value;
                                    a1=-tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value;
                                }
                                if(b<a1)
                                {
                                    tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=b;
                                    a1=b;
                                }
                            }
                            else 
                            {
                                a1=0; 
                                tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value="0";
                            }
                        }
                        else
                        {
                            a1=0; 
                            tr[i].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value="0";
                        }
                        sum1+=a1;
                    }
                    tr[tr.length-2].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value=sum1;
                    break;
                 }
                }
            }
            
        function getsum(no1,no2,no3)
        {
             var table=document.getElementById("tab5"); 
             var tr=table.getElementsByTagName("tr"); 
             var pattem=/^\d+(\.\d+)?$/; 
             var a;
             var b;
             var sum=0;
             for( i=1;i <tr.length-2;i++) 
             {
                 b=parseFloat(tr[i].getElementsByTagName("td")[no3].getElementsByTagName("span")[0].innerHTML); 
                 if(pattem.test(tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value.Trim()))
                   { 
                        tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value.Trim();
                        a=parseFloat(tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value); 
                        if(a<0)
                        {
                            tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=-tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value;
                            a=-tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value;
                        }
                        if(a>b)
                        {
                            tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value=0;
                            a=0;
                        }
                    }
                    else 
                    {
                        a=0; 
                        tr[i].getElementsByTagName("td")[no1].getElementsByTagName("input")[0].value="0";
                    }
                    sum+=a;
            }
            tr[tr.length-2].getElementsByTagName("td")[no1-no2].getElementsByTagName("input")[0].value=sum.toFixed(4);

        }
        
        
            function changecolor1(obj)
            {
                var clickrow=obj.parentNode.parentNode;
                var clickrowno=clickrow.getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML;
                var table=document.getElementById("tab1"); 
                var tr=table.getElementsByTagName("tr"); 
                for( i=1;i <tr.length-2;i++) 
                    { 
                       if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].innerHTML==clickrowno)
                        { 
                            tr[i].style.backgroundColor ='Orange'; 
                           
                        }
                        else 
                        {
                            tr[i].style.backgroundColor ='';
                        }
                     }
            }
            function checkme(obj)
            {
                var  usenum;
                var  usefznum;
                if(obj.checked) 
                { 
                   usenum=obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML;
                   usefznum=obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("span")[0].innerHTML;
                } 
                else 
                { 
                   usenum=0;
                   usefznum=0;
                }
                 obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=usenum;
                 obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=usefznum;
                 var table=document.getElementById("tab"); 
                 var tr=table.getElementsByTagName("tr"); 
                 var pattem=/^\d+(\.\d+)?$/; 
                 var a1;
                 var a2;
                 var sum1=0;
                 var sum2=0;
                 for( i=1;i <tr.length-2;i++) 
                   {
                        a1=parseFloat(tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value); 
                        a2=parseFloat(tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value); 
                        sum1+=a1;
                        sum2+=a2;
                    }
                    tr[tr.length-2].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=sum1.toFixed(4);
                    tr[tr.length-2].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=sum2.toFixed(0);
            }
            
            function checkmebzj(obj)
            {
                var  usenum;
                if(obj.checked) 
                { 
                   usenum=obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML;
                } 
                else 
                { 
                   usenum=0;
                }
                 obj.parentNode.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=usenum;
                 var table=document.getElementById("tab5"); 
                 var tr=table.getElementsByTagName("tr"); 
                 var pattem=/^\d+(\.\d+)?$/; 
                 var a1;
                 var sum1=0;
                 for( i=1;i <tr.length-2;i++) 
                   {
                        a1=parseFloat(tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value); 
                        sum1+=a1;
                    }
                    tr[tr.length-2].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value=sum1.toFixed(4);
            }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btn_concel" runat="server" Text="取消" OnClick="btn_concel_Click" />&nbsp;<%--必须先取消再占用--%>
                                <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click" />&nbsp;
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;
                                <asp:RadioButton ID="rad_automod" runat="server" GroupName="zymod" Text="自动模式" TextAlign="Right"
                                    Checked="true" Visible="false" />
                                <asp:RadioButton ID="rad_manmod" runat="server" GroupName="zymod" Text="手动模式" TextAlign="Right"
                                    Visible="false" />
                               <%-- <asp:RadioButton ID="rad_summess" runat="server" GroupName="messmod" Text="汇总信息"
                                    TextAlign="Right"  AutoPostBack="true" OnCheckedChanged="rad_summess_OnCheckedChanged"  Visible="false"/>--%>
                                <asp:RadioButton ID="rad_detail" runat="server" GroupName="messmod" Text="详细信息" TextAlign="Right"
                                    AutoPostBack="true"  OnCheckedChanged="rad_detail_OnCheckedChanged"  Checked="true" Visible="false"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
               <div style="overflow: auto; width: 100%">
                   <div class="cpbox xscroll">
                        <div width="100%">
                            <table width="100%">
                                <tr>
                                    <td style="font-size: small; text-align: center;" colspan="4">
                                        物料需用信息
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;单&nbsp;&nbsp;&nbsp;&nbsp;人:
                                        <asp:TextBox ID="tb_peoname" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_peoid" runat="server" Text="" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;单&nbsp;&nbsp;日&nbsp;&nbsp;期:<asp:TextBox ID="Tb_shijian"
                                            runat="server" Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                       &nbsp;&nbsp;&nbsp;批&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号:<asp:TextBox ID="TextBox_pid"
                                            runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;生&nbsp;&nbsp;产&nbsp;&nbsp;制&nbsp;&nbsp;号:
                                        <asp:TextBox ID="tb_zh" runat="server" Text="" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目:<asp:TextBox
                                            ID="tb_pjinfo" runat="server" Text="" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="tb_pjid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_pjname" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                    <td style="width: 34%;" align="left">
                                      &nbsp;&nbsp;&nbsp;工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;程:<asp:TextBox
                                            ID="tb_enginfo" runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="tb_engid" runat="server" Visible="false" Text=""></asp:TextBox>
                                        <asp:TextBox ID="tb_engname" runat="server" Visible="false" Text=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%">
                            <tr id="manmodtr" runat="server">
                                <td style="width: 100%">
                                    <table id="tab1" width="100%" class="nowrap cptable fullwidth">
                                        <asp:Repeater ID="tbpc_manmodmarrepallRepeater" runat="server" OnItemDataBound="tbpc_manmodmarrepallRepeater_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                    <td>
                                                        <strong>行号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>计划号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>物料编码</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格</strong>
                                                    </td>
                                                    <td>
                                                        <strong>材质</strong>
                                                    </td>
                                                    <td>
                                                        <strong>国标</strong>
                                                    </td>
                                                    <td>
                                                        <strong>单位</strong>
                                                    </td>
                                                    <td>
                                                        <strong>数量</strong>
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <strong>辅助数量</strong>
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <strong>辅助单位</strong>
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <strong>长度</strong>
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <strong>宽度</strong>
                                                    </td>
                                                    <td>
                                                        <strong>使用数量</strong>
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <strong>使用辅助数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>备注</strong>
                                                    </td>
                                                    <td>
                                                        <strong>状态</strong>
                                                    </td>
                                                    <td>
                                                        <strong>编辑</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget">
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_NUM" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:Label ID="PUR_FZNUM" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:Label ID="PUR_FZNUNIT" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_USTNUM" runat="server" Text='<%#Eval("usenum")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:Label ID="PUR_USTFZNUM" runat="server" Text='<%#Eval("usefznum")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="PUR_NOTE" runat="server" Text='<%#Eval("allnote")%>'></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PUR_STATETEXT" runat="server" Text='<%#get_all_state(Eval("allstata").ToString())%>'></asp:Label>
                                                        <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("allstata")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_edit" runat="server" Text="请点击" OnClick="btn_edit_Click" BorderStyle="None" />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr align="center">
                                                    <td rowspan="1" colspan="8">
                                                        合计:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tb_ntotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                    <td runat="server" visible="false">
                                                        <asp:TextBox ID="tb_fztotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                   
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td colspan="16" align="center">
                                                <asp:Panel ID="NoDataPane1" runat="server" Visible="false">
                                                    没有数据！</asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            <tr id="tr5" runat="server" visible="false">
                                <td style="width: 100%">
                                    <table id="tab5" width="100%" class="nowrap cptable fullwidth">
                                        <asp:Repeater ID="tbpc_marrepallbzjdetailRepeater" runat="server" OnItemDataBound="tbpc_marrepallbzjdetailRepeater_ItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color:#5CACEE">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <strong>行号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>库存唯一标识符</strong>
                                                    </td>
                                                    <td id="td1" runat="server" visible="false">
                                                        <strong>库存唯一标识符</strong>
                                                    </td>
                                                    <td>
                                                        <strong>物料编码</strong>
                                                    </td>
                                                    <td>
                                                        <strong>材料名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>库存数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>使用数量</strong>
                                                    </td>
                                                    <td id="td2" runat="server" visible="false">
                                                        <strong>入库批号</strong>
                                                    </td>
                                                    <td id="td3" runat="server" visible="false">
                                                        <strong>仓库</strong>
                                                    </td>
                                                    <td id="td4" runat="server" visible="false">
                                                        <strong>仓位</strong>
                                                    </td>
                                                    <td>
                                                        <strong>状态</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                            Checked="true" onclick="checkmebzj(this)"></asp:CheckBox>&nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="ROWBGSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="SQCODE" runat="server" Text='<%#Eval("SQCODE")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td id="Td1" runat="server" visible="false">
                                                        <asp:Label ID="NEWSQCODE" runat="server" Text='<%#Eval("NEWSQCODE")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="MARID" runat="server" Text='<%#Eval("MARID")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="MARNAME" runat="server" Text='<%#Eval("MARNAME")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="NUM" runat="server" Text='<%#Eval("NUM")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LUSENUM" runat="server" Text='<%#Eval("USENUM")%>'></asp:Label>
                                                        <asp:TextBox ID="USENUM" runat="server" Text='<%#Eval("USENUM")%>' onclick="javascript:if (this.value==''){ this.value='0.00';}else{this.select();}"
                                                            onblur="getsum(6,4,5)"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                    <td id="Td2" runat="server" visible="false">
                                                        <asp:Label ID="LOTNUM" runat="server" Text='<%#Eval("LOTNUM")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td id="Td3" runat="server" visible="false">
                                                        <asp:Label ID="WAREHOUSEID" runat="server" Text='<%#Eval("WAREHOUSEID")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td id="Td4" runat="server" visible="false">
                                                        <asp:Label ID="POSITIONID" runat="server" Text='<%#Eval("POSITIONID")%>'></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="STATETEXT" runat="server" Text='<%#get_alldet_state(Eval("STATE").ToString())%>'></asp:Label>
                                                        <asp:Label ID="STATE" runat="server" Text='<%#Eval("STATE")%>' Visible="false"></asp:Label>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr align="center">
                                                    <td rowspan="1" colspan="5">
                                                        合计:
                                                    </td>
                                                    <td>
                                                    <%--详细的库存数量--%>
                                                        <asp:TextBox ID="tb_kntotal" runat="server" BorderStyle="None" Enabled="False" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    <%--详细的使用数量--%>
                                                        <asp:TextBox ID="tb_kuntotal" runat="server" BorderStyle="None" BackColor="#EFF3FB"></asp:TextBox>
                                                    </td>
                                                    <td colspan="1">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td colspan="8" align="center">
                                                <asp:Panel ID="Panel5" runat="server" Visible="false">
                                                    没有数据！</asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                   </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
