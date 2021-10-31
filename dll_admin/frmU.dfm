object frm: Tfrm
  Left = 745
  Top = 325
  Width = 324
  Height = 170
  Caption = 'frm'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object connect1: TUniConnection
    ProviderName = 'MySQL'
    Port = 3306
    Database = 'nalice'
    SpecificOptions.Strings = (
      'MySQL.Protocol=mpTCP')
    Username = 'manager'
    Server = 'localhost'
    LoginPrompt = False
    Left = 48
    Top = 8
    EncryptedPassword = 
      '96FF90FF99FFC8FFC9FF95FF92FF90FFC7FF97FF99FFCDFFCCFFCAFF86FF99FF' +
      '97FFCDFF91FF90FF97FF98FF90FF'
  end
  object qUSDJPY: TUniQuery
    Connection = connect1
    SQL.Strings = (
      'call get_forecast('#39'De4HjvDLbU'#39', '#39'USDJPY'#39')')
    Left = 256
    Top = 8
  end
  object qGBPUSD: TUniQuery
    Connection = connect1
    SQL.Strings = (
      'call get_forecast('#39'De4HjvDLbU'#39', '#39'GBPUSD'#39')')
    Left = 168
    Top = 8
  end
  object qClearGBPUSD: TUniQuery
    Connection = connect1
    SQL.Strings = (
      
        'call clear_forecast('#39'8fom23bh622heyGhbfu%Fg6f^UFrguFu6'#39', '#39'GBPUSD' +
        #39')')
    Left = 168
    Top = 64
  end
  object qClearUSDJPY: TUniQuery
    Connection = connect1
    SQL.Strings = (
      
        'call clear_forecast('#39'8fom23bh622heyGhbfu%Fg6f^UFrguFu6'#39', '#39'USDJPY' +
        #39')')
    Left = 256
    Top = 64
  end
  object mysqlnprvdr1: TMySQLUniProvider
    Left = 64
    Top = 80
  end
end
